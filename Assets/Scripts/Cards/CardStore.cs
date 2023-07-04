using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Reflection;
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

    private Dictionary<string, Card> cardBox;
    private Dictionary<string, ConstructorInfo> cardCtors;
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
    private void ReadCard()
    {
        cardBox = new Dictionary<string, Card>();
        cardCtors = new Dictionary<string, ConstructorInfo>();
        var ct = typeof(Card);
        var ass = ct.Assembly;
        var types = ass.GetTypes();
        foreach (var type in types)
        {
            if (type.IsSubclassOf(ct))
            {
                var ctor = type.GetConstructor(new Type[] { typeof(CardInfo) });
                if (ctor != null)
                {
                    Card card = (Card)ctor.Invoke(new object[] { null });
                    cardBox[card.name] = card;
                    cardCtors[card.name] = ctor;
                }
            }
        }
    }
    public CardVisual CreateCardVisual(Card card)
    {
        GameObject visual;
        switch (card.type)
        {
            case CardType.Enemy:
            case CardType.EnemyDerive:
                visual = GameObject.Instantiate(enemyPrefab);
                break;
            case CardType.Monster:
            case CardType.FriendlyDerive:
            case CardType.Construction:
                visual = GameObject.Instantiate(monsterPrefab);
                break;
            case CardType.Spell:
                visual = GameObject.Instantiate(spellPrefab);
                break;
            default: throw new Exception($"错误的卡牌类型{card.type}");
        }
        var v = visual.GetComponent<CardVisual>();
        card.Init();
        v.SetCard(card);
        return v;
    }
    public Card CreateCard(CardInfo info, bool withVisual = true)
    {
        if (cardBox == null) ReadCard();
        if (cardBox.ContainsKey(info.name))
        {
            var card = cardCtors[info.name].Invoke(new object[] { info }) as Card;
            if (withVisual) CreateCardVisual(card);
            return card;
        }
        throw new Exception($"不存在{info.name}卡牌");
    }

    public Card CreateCard(string name, bool withVisual = true) => CreateCard(new CardInfo() { name = name }, withVisual);
    //public void LoadCardData()
    //{
    //    var monsterFiles = Directory.GetFiles(MonsterDataPath);
    //    foreach (var file in monsterFiles)
    //    {
    //        var text = File.ReadAllText(file);
    //        var info = JsonUtility.FromJson<MonsterCardInfo>(text);
    //        monsterCardInfos.Add(info.name, info);
    //    }
    //    var spellFiles = Directory.GetFiles(SpellDataPath);
    //    foreach (var file in spellFiles)
    //    {
    //        var text = File.ReadAllText(file);
    //        var info = JsonUtility.FromJson<SpellCardInfo>(text);
    //        spellCardInfos.Add(info.name, info);
    //    }
    //    var enemyFiles = Directory.GetFiles(EnemyDataPath);
    //    foreach (var file in enemyFiles)
    //    {
    //        var text = File.ReadAllText(file);
    //        var info = JsonUtility.FromJson<EnemyCardInfo>(text);
    //        CardInfos.Add(info.name, info);
    //    }
    //}

    //public Card CopyCard(string name)
    //{
    //    if (monsterCardInfos.ContainsKey(name))
    //        return CreateCard(monsterCardInfos[name]);
    //    else if (monsterCardInfos.ContainsKey(name))
    //        return CreateCard(spellCardInfos[name]);
    //    else if (CardInfos.ContainsKey(name))
    //        return CreateCard(CardInfos[name]);
    //    throw new Exception($"未定义{name}卡牌");
    //}

    //public GameObject CreateCard(string key, string[] args)
    //{
    //    Card card = new Card(key);
    //    GameObject go = null;
    //    switch (key)
    //    {
    //        #region 法术
    //        case "打击":
    //            card.AddComponnet(new SpellCastComponent(1, 4, new SingleDamage2SpecifyEnemy(6)));
    //            card.type = CardType.Spell;
    //            go = GameObject.Instantiate(spellPrefab);
    //            break;
    //        case "交换":
    //            card.AddComponnet(new SpellCastComponent(1, 3, new SwapMonster()));
    //            card.type = CardType.Spell;
    //            go = GameObject.Instantiate(spellPrefab);
    //            break;
    //        #endregion
    //    }
    //    card.Init();
    //    go.GetComponent<CardVisual>().SetCard(card);

    //    return go;
    //}
}