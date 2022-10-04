using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public enum GamePhase
{
    gameStart,playerDraw,playerAction,enemyAction
}

public class BattleManager : MonoSingleton<BattleManager>
{
    #region 基本数据

    public GameObject DataManger;
    private PlayerData playerData;
    private EnemyData enemyData;
    public List<Card> playerBattleDeckList = new List<Card>();
    public List<Enemy> EnemyBattleList = new List<Enemy>();
    public List<GameObject> EnemyDisplayList = new List<GameObject>();

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

    public GamePhase GamePhase = GamePhase.gameStart;
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

    public UnityEvent phaseChangeEvent = new UnityEvent();





    // Start is called before the first frame update
    void Start()
    {
        playerData = DataManger.GetComponent<PlayerData>();
        cellManager = CellManager.GetComponent<CellManager>();
        enemyData = DataManger.GetComponent<EnemyData>();

        GameStart();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            DestroyArrow();
            waitingSummonMonster = null;
            CloseCellsAndEnemies();
        }
    }

    //开始游戏
    //载入数据，卡组洗牌,初始手牌
    //游戏阶段

    public void GameStart()
    {
        handCardNumber=0;
        ReadDeck();
        ReadEnemyBoard();
        //读取数据
        ShuffletDeck();
        //卡组洗牌
        LoadCells();
        //加载celllist


        DrawCard(drawNumber);
        DrawEnemy(enemyNumber);


        GamePhase = GamePhase.playerDraw;
        phaseChangeEvent.Invoke();

    }

    public void LoadCells()
    {
        cellList = cellManager.GetCellList();
    }

    public void OnPlayerDraw()
    {
        DrawCard(1);
        GamePhase = GamePhase.playerAction;
        phaseChangeEvent.Invoke();
    }

    public void ReadDeck()
    {
        //加载玩家卡组
        foreach(var id in playerData.playerCardIds)
        {
            //Debug.Log("读到卡：");
            //Debug.Log(id);
            int count = playerData.playerCards[id];
            for (int i =0;i<count;i++)
            {
                playerBattleDeckList.Add(playerData.CardStore.CopyCard(id));
            }
        }
    }

    public void ReadEnemyBoard()
    {
        foreach(var id in enemyData.enemyIdList)
        {
            //Debug.Log("读到敌人：");
            //Debug.Log(id);
            EnemyBattleList.Add(enemyData.allEnemyBox[id]);
        }

    }

    public void ShuffletDeck()

    {
        List<Card> shuffletDeck =  playerBattleDeckList;

        for(int i=0; i<shuffletDeck.Count; i++)
        {
            int rad=Random.Range(0,shuffletDeck.Count);
            Card temp = shuffletDeck[i];
            shuffletDeck[i]=shuffletDeck[rad];
            shuffletDeck[rad]=temp;

        }
    }

    public void DrawCard(int _count)
    {
        List<Card> drawDeck = playerBattleDeckList;
        Transform handBoardPos = handBoard;


        for (int i =0; i< _count; i++)
        {
            GameObject card = Instantiate(cardPrefab,handBoardPos);
            card.GetComponent<CardDisplay>().card =  drawDeck[0];
            drawDeck.RemoveAt(0);
            handCardNumber +=1;
        }

        handCounterText.text = (handCardNumber.ToString()+"/"+handMaxNumber.ToString());
    }

    public void DrawEnemy(int _count)
    {
        List<Enemy> enemies = EnemyBattleList;
        Transform enemyPos = enemyBoard;


        for (int i =0; i< _count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab,enemyPos);
            enemy.GetComponent<EnemyDisplay>().enemy =  enemies[0];
            //Debug.Log(enemy.GetComponent<EnemyDisplay>().enemy.enemyName);
            EnemyDisplayList.Add(enemy);
            enemies.RemoveAt(0);
        }

        //Debug.Log("现有enemy：");
        //Debug.Log(EnemyDisplayList[0].GetComponent<EnemyDisplay>().enemy.enemyName);

    }

    public void TurnEnd()
    {
        if(GamePhase==GamePhase.playerAction)
        {
            GamePhase = GamePhase.enemyAction;
        }

        if(GamePhase ==  GamePhase.enemyAction)
        {
            GamePhase = GamePhase.playerDraw;
        }
    }

#region 入场相关方法
    public void SummonRequest(GameObject _monsterCard)
    {  
        bool hasEmptyBlock = false;

        foreach(var cell in cellList)
        {
            if(cell.GetComponent<Cell>().card == null) //判断响应格子是否空
            {
                cell.GetComponent<Cell>().SummonCell.SetActive(true);
                //等待召唤显示
                hasEmptyBlock = true;
            }
        }

        if(hasEmptyBlock)
        {
            waitingSummonMonster = _monsterCard;
            CreatArrow(_monsterCard.transform , ArrowPrefab);
            //Debug.Log("坐标x：");
            //Debug.Log(_monsterCard.transform.position.x);
            //Debug.Log("坐标y：");
            //Debug.Log(_monsterCard.transform.position.y);
        }
    }

    public void SummonConfirm(GameObject _cell) 
    {
        waitingSummonMonster.GetComponent<BattleCard>().nowCell = _cell;
        Summon(waitingSummonMonster, _cell);
        CloseCellsAndEnemies();
        DestroyArrow();
        waitingSummonMonster = null;
    }

    public void Summon(GameObject _monster , GameObject _cell)
    {
        _monster.transform.SetParent(_cell.transform);
        _monster.transform.localPosition = Vector3.zero;
        _monster.GetComponent<BattleCard>().state = BattleCardState.onBoard;
        _cell.GetComponent<Cell>().card = _monster;

    }

#endregion

#region 场上相关方法
    public void OnBoardRequest(GameObject _monsterCard)
    {
        bool hasEmptyAdjCell = false;
        GameObject nowCell = _monsterCard.transform.parent.gameObject;

        List<GameObject> adjList = cellManager.GetAdjCells(nowCell);
        int nowRange = cellManager.getRange(nowCell);

        foreach (var cell in adjList)
        {
            if(cell.GetComponent<Cell>().card == null) //判断响应格子是否空
            {
                cell.GetComponent<Cell>().SummonCell.SetActive(true);
                //等待移动显示
                hasEmptyAdjCell= true;
            }
        }

        if(_monsterCard.GetComponent<BattleCard>().atkRange>=nowRange)
        {
            foreach(var enemy in EnemyDisplayList)
            {
                //Debug.Log("foreach里面有：");
                //Debug.Log(enemy.GetComponent<EnemyDisplay>().enemy.enemyName);
                enemy.GetComponent<AttackTarget>().attackable = true;
                enemy.GetComponent<EnemyDisplay>().selectedEdge.SetActive(true);
            }
        }



        if(hasEmptyAdjCell)
        {
            waitingOnBoardMonster = _monsterCard;
            CreatArrow(_monsterCard.transform , ArrowPrefab);
        }
    }

    public void OnBoardConfirm(GameObject _cell)
    {
        GameObject lastCell = waitingOnBoardMonster.GetComponent<BattleCard>().nowCell;
        lastCell.GetComponent<Cell>().card = null;
        //Debug.Log("lastcell.card:");
        //Debug.Log(lastCell.GetComponent<Cell>().card);
        waitingOnBoardMonster.GetComponent<BattleCard>().nowCell = _cell;
        Summon(waitingOnBoardMonster, _cell);
        CloseCellsAndEnemies();
        DestroyArrow();
        waitingOnBoardMonster= null;
    }

    public void AttackConfirm(GameObject _target)
    {
        Attack(waitingOnBoardMonster, _target);
        CloseCellsAndEnemies();
        DestroyArrow();
        waitingOnBoardMonster= null;

    }

    public void Attack(GameObject _attacker,GameObject _target)
    {
        MonsterCard attacker = _attacker.GetComponent<CardDisplay>().card as MonsterCard;
        _target.GetComponent<AttackTarget>().ApplyDamage(attacker.attack);
        _target.GetComponent<EnemyDisplay>().showEnemy();
    }

    public void OnBoardMove()
    {

    }

#endregion     
    public void CreatArrow(Transform _startPoint,GameObject _prefab)
    {
        arrow = GameObject.Instantiate(_prefab, _startPoint);
        arrow.GetComponent<Arrow>().SetStartPoint(new Vector2(_startPoint.position.x,_startPoint.position.y));

    }
    
    public void DestroyArrow()
    {
        Destroy(arrow);
    }

    public void CloseCellsAndEnemies()
    {
        foreach(var cell in cellList)
        {
            cell.GetComponent<Cell>().SummonCell.SetActive(false);
        }
        foreach(var enemy in EnemyDisplayList)
        {
            enemy.GetComponent<AttackTarget>().attackable = false;
            enemy.GetComponent<EnemyDisplay>().selectedEdge.SetActive(false);
        }
    }

    public int GetWaitingCardState()
    {
        if(waitingSummonMonster!=null) return 1;
        else if(waitingOnBoardMonster!= null) return 2;
        else return 0;
    }
}
