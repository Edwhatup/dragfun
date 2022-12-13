using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//控制对敌人的可视化和敌人的回合管理
public class EnemyManager : MonoBehaviour, IManager
{
    public static EnemyManager Instance { get; private set; }
    public List<Card> enemies = new List<Card>();
    List<Card> tombs = new List<Card>();
    [SerializeField]
    TextAsset enemyData;
    [SerializeField]
    RectTransform enemyBoardTrans;
    Dictionary<Card, GameObject> enemyObjects = new Dictionary<Card, GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #region 获取敌人信息的方法
    /// <summary>
    /// 获取满足条件的所有敌人
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Card> GetAllSpecifyEnemies(Func<Card, bool> condition)
    {
        return enemies.FindAll((e) => condition.Invoke(e));
    }
    /// <summary>
    /// 随机获取满足条件的n名敌人,现有敌人数目不够n时返回所有满足条件的敌人 
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Card> GetRandomSpecifyEnemies(Func<Card, bool> condition, int n)
    {
        return enemies.FindAll(e => condition.Invoke(e))
                      .GetRandomItems(n);
    }
    /// <summary>
    /// 随机获取满足条件的1名敌人,没有敌人时返回null
    /// </summary>
    /// <param name="condition">条件</param>
    public Card GetRandomSpecifyEnemy(Func<Card, bool> condition)
    {
        return enemies.FindAll(e => condition.Invoke(e))
                      .GetRandomItem();
    }
    /// <summary>
    /// 获取符合比较条件的最大存活敌人,没有敌人时返回null
    /// </summary>
    /// <param name="comparer">比较条件</param>
    public Card GetMaxEnemy(Func<Card, Card, int> comparer)
    {
        return enemies.GetMaxItem(comparer);
    }
    /// <summary>
    /// 获取符合比较条件的最小存活敌人,没有敌人时返回null
    /// </summary>
    /// <param name="comparer">比较条件</param>
    public Card GetMinEnemy(Func<Card, Card, int> comparer)
    {
        return enemies.GetMinItem(comparer);
    }
    /// <summary>
    /// 获取所有存活的敌人
    /// </summary>
    public List<Card> GetAllEnemies()
    {
        return enemies.ToList();
    }
    /// <summary>
    /// 随机获取1个存活的敌人
    /// </summary>
    public Card GetRandomEnemy()
    {
        return enemies.GetRandomItem();
    }
    /// <summary>
    /// 随机获取n个存活的敌人,现有敌人数目不够n时返回现有所有敌人 
    /// </summary>
    public List<Card> GetRandomEnemies(int n)
    {
        return enemies.GetRandomItems(n);
    }
    #endregion
    void LoadEnemyData()
    {
        string[] dataRow = enemyData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray.Length < 2 || rowArray[0] == "name")
                continue;
            var enemyGo = CardStore.Instance.CreateCard(rowArray[0],null);
            var enemy = enemyGo.GetComponent<CardVisual>().card;
            enemy.camp = CardCamp.Enemy;
            enemies.Add(enemy);
            enemyGo.transform.SetParent(enemyBoardTrans);
        }
    }
    #region IGameTurn 实现部分
    public void Refresh()
    {
        var tmpEnemies = enemies.ToList();
        foreach (var enemy in tmpEnemies)
        {
            if (enemy.field.state == BattleState.Dead)
            {
                if (enemyObjects.ContainsKey(enemy))
                {
                    Destroy(enemyObjects[enemy]);
                    enemyObjects.Remove(enemy);
                    enemies.Remove(enemy);
                    tombs.Add(enemy);
                }
            }
            else
            {
                enemy.visual.UpdateVisual();
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(enemyBoardTrans);
        if (enemies.Count == 0) GameManager.Instance.GamePass();
    }
    public void GameStart()
    {
        LoadEnemyData();
        Refresh();
    }
    public void BroadcastCardEvent(AbstractCardEvent cardEvent)
    {

    }
    #endregion
}