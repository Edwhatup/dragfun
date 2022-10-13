using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public enum GamePhase
{
    GameStart, PlayerDraw, PlayerAction, EnemyAction
}

public class GameManager : MonoSingleton<GameManager>,IGameTurn
{
    #region 基本数据
    List<IGameTurn> managers = new List<IGameTurn>();


    [SerializeField]
    CardStore cardStore;

    [SerializeField]
    EnemyManager enemyManager;

    #endregion



    public Transform handBoard;
    public Transform enemyBoard;
    //发牌区和怪显示区

    public List<GameObject> cellList;
    public GameObject CellManager;
    private CellManager cellManager;
    public GameObject cardPrefab;
    public GameObject enemyPrefab;
    //格子和牌的预组


    public GameObject playerLife;
    //玩家生命

    public GamePhase GamePhase = GamePhase.GameStart;
    //初始化阶段

    private GameObject waitingSummonMonster;
    //待召唤卡

    public GameObject ArrowPrefab;
    private GameObject arrow;
    //指示箭头

    private GameObject waitingOnBoardMonster;
    //待行为场上卡

    #region 管理抽牌、场上生成怪数量数据
    public int drawNumber = 5;
    public int enemyNumber = 1;
    public int handMaxNumber = 5;
    public int handCardNumber;
    public Text handCounterText;
    //管理抽牌数量

    #endregion

    //public UnityEvent phaseChangeEvent = new UnityEvent();

    public event Action<GamePhase, GamePhase> phaseChangeEvent;


    // Start is called before the first frame update
    void Start()
    {
        managers.Add(EnemyManager.Instance);
        //cellManager = CellManager.GetComponent<CellManager>();
       GameStart();

    }
    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButton(1))
    //    {
    //        DestroyArrow();
    //        waitingSummonMonster = null;
    //        CloseCellsAndEnemies();
    //    }
    //}

    //开始游戏
    //初始化玩家、卡牌和敌人状态
    public void GameStart()
    {
        SwicthGamePhase(GamePhase.GameStart);
        foreach (var i in managers)
            i.GameStart();
    }

    public void PlayerAction()
    {
        SwicthGamePhase(GamePhase.PlayerAction); 
        foreach (var i in managers)
            i.PlayerAction();
    }

    public void EnemyAction()
    {
        SwicthGamePhase(GamePhase.EnemyAction);
        foreach (var i in managers)
            i.EnemyAction();
    }

    public void PlayDraw()
    {
        SwicthGamePhase(GamePhase.PlayerDraw);
        foreach (var i in managers)
            i.PlayDraw();
    }
    private void SwicthGamePhase(GamePhase nextPhase)
    {
        phaseChangeEvent?.Invoke(GamePhase,nextPhase);
        GamePhase = nextPhase;
    }

    public void LoadCells()
    {
        cellList = cellManager.GetCellList();
    }


    //public void DrawEnemy(int _count)
    //{
    //    List<Enemy> enemies = EnemyBattleList;
    //    Transform enemyPos = enemyBoard;


    //    for (int i = 0; i < _count; i++)
    //    {
    //        GameObject enemy = Instantiate(enemyPrefab, enemyPos);
    //        enemy.GetComponent<EnemyDisplay>().enemy = enemies[0];
    //        //Debug.Log(enemy.GetComponent<EnemyDisplay>().enemy.enemyName);
    //        EnemyDisplayList.Add(enemy);
    //        enemies.RemoveAt(0);
    //    }

    //    //Debug.Log("现有enemy：");
    //    //Debug.Log(EnemyDisplayList[0].GetComponent<EnemyDisplay>().enemy.enemyName);
    //}

    public void TurnEnd()
    {
        if (GamePhase == GamePhase.PlayerAction)
        {
            GamePhase = GamePhase.EnemyAction;
        }

        if (GamePhase == GamePhase.EnemyAction)
        {
            GamePhase = GamePhase.PlayerDraw;
        }
    }

    #region 入场相关方法
    //public void SummonRequest(GameObject _monsterCard)
    //{
    //    bool hasEmptyBlock = false;

    //    foreach (var cell in cellList)
    //    {
    //        if (cell.GetComponent<Cell>().card == null) //判断响应格子是否空
    //        {
    //            cell.GetComponent<Cell>().SummonCell.SetActive(true);
    //            //等待召唤显示
    //            hasEmptyBlock = true;
    //        }
    //    }

    //    if (hasEmptyBlock)
    //    {
    //        waitingSummonMonster = _monsterCard;
    //        CreatArrow(_monsterCard.transform, ArrowPrefab);
    //        //Debug.Log("坐标x：");
    //        //Debug.Log(_monsterCard.transform.position.x);
    //        //Debug.Log("坐标y：");
    //        //Debug.Log(_monsterCard.transform.position.y);
    //    }
    //}

    //public void SummonConfirm(GameObject _cell)
    //{
    //    waitingSummonMonster.GetComponent<BattleCard>().nowCell = _cell;
    //    Summon(waitingSummonMonster, _cell);
    //    CloseCellsAndEnemies();
    //    DestroyArrow();
    //    waitingSummonMonster = null;
    //}

    //public void Summon(GameObject _monster, GameObject _cell)
    //{
    //    _monster.transform.SetParent(_cell.transform);
    //    _monster.transform.localPosition = Vector3.zero;
    //    _monster.GetComponent<BattleCard>().state = BattleCardState.onBoard;
    //    _cell.GetComponent<Cell>().card = _monster;

    //}

    #endregion

    #region 场上相关方法
    //public void OnBoardRequest(GameObject _monsterCard)
    //{
    //    bool hasEmptyAdjCell = false;
    //    GameObject nowCell = _monsterCard.transform.parent.gameObject;

    //    List<GameObject> adjList = cellManager.GetAdjCells(nowCell);
    //    int nowRange = cellManager.getRange(nowCell);

    //    foreach (var cell in adjList)
    //    {
    //        if (cell.GetComponent<Cell>().card == null) //判断响应格子是否空
    //        {
    //            cell.GetComponent<Cell>().SummonCell.SetActive(true);
    //            //等待移动显示
    //            hasEmptyAdjCell = true;
    //        }
    //    }

    //    if (_monsterCard.GetComponent<BattleCard>().atkRange >= nowRange)
    //    {
    //        foreach (var enemy in EnemyDisplayList)
    //        {
    //            //Debug.Log("foreach里面有：");
    //            //Debug.Log(enemy.GetComponent<EnemyDisplay>().enemy.enemyName);
    //            enemy.GetComponent<AttackTarget>().attackable = true;
    //            enemy.GetComponent<EnemyDisplay>().selectedEdge.SetActive(true);
    //        }
    //    }



    //    if (hasEmptyAdjCell)
    //    {
    //        waitingOnBoardMonster = _monsterCard;
    //        CreatArrow(_monsterCard.transform, ArrowPrefab);
    //    }
    //}

    //public void OnBoardConfirm(GameObject _cell)
    //{
    //    GameObject lastCell = waitingOnBoardMonster.GetComponent<BattleCard>().nowCell;
    //    lastCell.GetComponent<Cell>().card = null;
    //    //Debug.Log("lastcell.card:");
    //    //Debug.Log(lastCell.GetComponent<Cell>().card);
    //    waitingOnBoardMonster.GetComponent<BattleCard>().nowCell = _cell;
    //    Summon(waitingOnBoardMonster, _cell);
    //    CloseCellsAndEnemies();
    //    DestroyArrow();
    //    waitingOnBoardMonster = null;
    //}

    //public void AttackConfirm(GameObject _target)
    //{
    //    Attack(waitingOnBoardMonster, _target);
    //    CloseCellsAndEnemies();
    //    DestroyArrow();
    //    waitingOnBoardMonster = null;

    //}

    //public void Attack(GameObject _attacker, GameObject _target)
    //{
    //    BattleCard attacker = _attacker.GetComponent<CardDisplay>().card as BattleCard;
    //    //_target.GetComponent<AttackTarget>().ApplyDamage(attacker.);
    //    _target.GetComponent<EnemyDisplay>().showEnemy();
    //}

    //public void OnBoardMove()
    //{

    //}

    #endregion
    public void CreatArrow(Transform _startPoint, GameObject _prefab)
    {
        arrow = GameObject.Instantiate(_prefab, _startPoint);
        arrow.GetComponent<Arrow>().SetStartPoint(new Vector2(_startPoint.position.x, _startPoint.position.y));

    }

    public void DestroyArrow()
    {
        Destroy(arrow);
    }

    public void CloseCellsAndEnemies()
    {
        foreach (var cell in cellList)
        {
            cell.GetComponent<Cell>().SummonCell.SetActive(false);
        }
        //foreach (var enemy in EnemyDisplayList)
        //{
        //    enemy.GetComponent<AttackTarget>().attackable = false;
        //    enemy.GetComponent<EnemyDisplay>().selectedEdge.SetActive(false);
        //}
    }

    public int GetWaitingCardState()
    {
        if (waitingSummonMonster != null) return 1;
        else if (waitingOnBoardMonster != null) return 2;
        else return 0;
    }

    
}
