using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class CardStore : MonoBehaviour
{
    public static CardStore Instance { get; private set; }
    string MonsterDataPath;
    string SpellDataPath;
    string EnemyDataPath;
    [SerializeField]
    GameObject monsterPrefab;
    [SerializeField]
    GameObject spellPrefab;
    [SerializeField]
    GameObject enemyPrefab;


    private Dictionary<string, MonsterCardInfo> monsterCardInfos = new Dictionary<string, MonsterCardInfo>();
    private Dictionary<string, SpellCardInfo> spellCardInfos = new Dictionary<string, SpellCardInfo>();
    private Dictionary<string, EnemyCardInfo> CardInfos = new Dictionary<string, EnemyCardInfo>();


    void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
        {
            MonsterDataPath = Path.Combine(Application.dataPath, "Datas", "Monster");
            SpellDataPath = Path.Combine(Application.dataPath, "Datas", "Spell");
            EnemyDataPath = Path.Combine(Application.dataPath, "Datas", "Enemy");
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //LoadCardData();
        }
    }
    public void LoadCardData()
    {
        var monsterFiles = Directory.GetFiles(MonsterDataPath);
        foreach (var file in monsterFiles)
        {
            var text = File.ReadAllText(file);
            var info = JsonUtility.FromJson<MonsterCardInfo>(text);
            monsterCardInfos.Add(info.name, info);
        }
        var spellFiles = Directory.GetFiles(SpellDataPath);
        foreach (var file in spellFiles)
        {
            var text = File.ReadAllText(file);
            var info = JsonUtility.FromJson<SpellCardInfo>(text);
            spellCardInfos.Add(info.name, info);
        }
        var enemyFiles = Directory.GetFiles(EnemyDataPath);
        foreach (var file in enemyFiles)
        {
            var text = File.ReadAllText(file);
            var info = JsonUtility.FromJson<EnemyCardInfo>(text);
            CardInfos.Add(info.name, info);
        }
    }

    private Card CreateCard(MonsterCardInfo info)
    {
        Card card = new Card(info.name);
        card.AddComponnet(new AttackComponent(info.atk, info.atkRange, info.atkTimes, info.canAtk, info.atkCost, info.atkFree));
        card.AddComponnet(new AttackedComponent(info.hp, info.bless));

        return card;
    }
    private Card CreateCard(SpellCardInfo info)
    {
        Card card = new Card(info.name);
        return card;
    }
    private Card CreateCard(EnemyCardInfo info)
    {
        Card card = new Card(info.name);
        return card;
    }

    public Card CopyCard(string name)
    {
        if (monsterCardInfos.ContainsKey(name))
            return CreateCard(monsterCardInfos[name]);
        else if (monsterCardInfos.ContainsKey(name))
            return CreateCard(spellCardInfos[name]);
        else if (CardInfos.ContainsKey(name))
            return CreateCard(CardInfos[name]);
        throw new Exception($"未定义{name}卡牌");
    }

    public GameObject CreateCard(string key, string[] args)
    {
        Card card = new Card(key);
        GameObject go = null;
        switch (key)
        {
            #region 随从
            case "狂战士":
                card.AddComponnet(new AttackComponent(2));
                card.AddComponnet(new AttackedComponent(3));
                card.AddComponnet(new OnMove1Listener(2, 2));
                card.AddComponnet(new FieldComponnet());
                card.AddComponnet(new SummonComponent(1));
                card.type = CardType.Monster;
                go = GameObject.Instantiate(monsterPrefab);
                break;
            case "八里湾":
                card.AddComponnet(new AttackComponent(3));
                card.AddComponnet(new AttackedComponent(3));
                card.AddComponnet(new FieldComponnet());
                card.AddComponnet(new SummonComponent(1));
                card.type = CardType.Monster;
                go = GameObject.Instantiate(monsterPrefab);
                break;
            #endregion
            #region 衍生物
            case "法术残骸":
                card.AddComponnet(new FieldComponnet());
                card.AddComponnet(new WreckComponent(args));
                card.type = CardType.Derive;
                go = GameObject.Instantiate(monsterPrefab);
                break;
            #endregion
            #region 法术
            case "打击":
                card.AddComponnet(new SpellCastComponent(1, 4, new SingleDamage2SpecifyEnemy(6)));
                card.type = CardType.Spell;
                go = GameObject.Instantiate(spellPrefab);
                break;
            case "交换":
                card.AddComponnet(new SpellCastComponent(1, 3, new SwapMonster()));
                card.type = CardType.Spell;
                go = GameObject.Instantiate(spellPrefab);
                break;
            #endregion
            #region 怪物
            case "地精大块头":
                card.AddComponnet(new AttackedComponent(50));
                card.AddComponnet(new FieldComponnet());
                card.type = CardType.Enemy;
                go = GameObject.Instantiate(enemyPrefab);
                break;
            #endregion
        }
        card.Init();
        go.GetComponent<CardVisual>().SetCard(card);

        return go;
    }
}