using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour, IManager
{
    public static TurnManager Instance { get; private set; }

    [SerializeField] private int turnPP = 10, drawCnt = 1;

    private int turn = 0;

    private void Awake()
    {
        // if (Instance == null)
        // {
        //     Instance = this;
        //     EventManager.Instance.eventListen += BroadcastCardEvent;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else Destroy(gameObject);
        EventManager.Instance.eventListen += BroadcastCardEvent;
        Instance = this;
    }

    public void GameStart()
    {
        turn = 0;
        GameManager.Instance.Pp = turnPP;
    }

    public void Refresh() { }
    public void BroadcastCardEvent(AbstractCardEvent cardEvent) { }

    public int GetTurn() => turn + 1;
    public void NextTurn()
    {
        turn++;
        var deltaPP = GameManager.Instance.Pp;
        GameManager.Instance.Pp = turnPP;
        CardManager.Instance.DrawCard(drawCnt);
        CardManager.Instance.board.ForEach(i => i.TurnReset());

        GameManager.Instance.BroadcastCardEvent(new EndTurnEvent(deltaPP));
    }
}
