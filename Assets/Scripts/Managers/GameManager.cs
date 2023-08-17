﻿using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IManager
{
    public SystemRandom Random = new SystemRandom();
    public enum GamePhase
    {
        ReadyStart,
        InMap,
        Battle
    }
    GamePhase phase = GamePhase.ReadyStart;
    public static GameManager Instance { get; private set; }
    public int Pp
    {
        get => pp;
        set => pp = value;
    }

    public int LifeEnergyPoint
    {
        get => lifeEnergyPoint;
        set { lifeEnergyPoint = value; UpdateStateUI(); }
    }

    private int pp = 1000000;
    private int lifeEnergyPoint = 0;
    void Awake()
    {
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(this);
        // }
        // else Destroy(Instance);
        Instance = this;
    }
    List<IManager> managers = new List<IManager>();
    // Start is called before the first frame update
    void Start()
    {
        managers.Add(CellManager.Instance);
        managers.Add(CardManager.Instance);
        managers.Add(TurnManager.Instance);
    }

    #region IGameTurn 实现
    public void GameStart()
    {
        foreach (var i in managers)
            i.GameStart();
        //pp = RoundPP;
        Refresh();
    }
    public void Refresh()
    {
        foreach (var i in managers)
            i.Refresh();

        // this.pp = CardManager.Instance.Enemies.GetMinItem((l, r) =>
        //                 l.enemyAction.current.timer - r.enemyAction.current.timer)
        //                 .enemyAction.current.timer;
    }

    #endregion

    #region 回合管理相关
    public void Click2GameStart()
    {
        // Debug.Log(phase);
        if (phase == GamePhase.ReadyStart)
        {
            phase = GamePhase.Battle;
            GameStart();
        }
    }

    public void SwitchGameObjState(GameObject go) { go.SetActive(!go.activeInHierarchy); }

    public void GameFalse()
    {
        Console.WriteLine("游戏失败");
    }

    public void GamePass()
    {
        Debug.Log("游戏通关");
        Player.Instance.CardDeck.ForEach(i => Destroy(i.visual));
        SceneManager.LoadScene("Shop");
    }

    public void BroadcastCardEvent(AbstractCardEvent e)
    {
        foreach (var i in managers)
            i.BroadcastCardEvent(e);
    }

    #endregion

    public bool CheckPP(int amount) => Pp >= amount;
    public bool TryCostPP(int amount)
    {
        if (CheckPP(amount))
        {
            Pp -= amount;
            return true;
        }
        return false;
    }

    private void UpdateStateUI()
    {
        if (LifeEnergyPoint > 0) LifeEnergyText.Instance.ShowText();
    }

}