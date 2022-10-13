using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class CardStore : MonoSingleton<CardStore>
{
    public TextAsset cardData;
    public Dictionary<string, Card> cardBox = new Dictionary<string, Card>();
    private Dictionary<string,object[]> cardParams=new Dictionary<string, object[]>();
    private List<string> cardNames = new List<string>();
    private Dictionary<string,Type> cardTypes = new Dictionary<string,Type>();
    new void Awake()
    {
        base.Awake();
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        LoadCardData();
    }
    public void LoadCardData()
    {
        cardNames.Clear();
        cardBox.Clear();
        cardParams.Clear();
        string[] dataRow = cardData.text.Split('\n');
        cardTypes = GetAllCardTypes();
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray.Length <= 1 || rowArray[0] == "#")
            {
                continue;
            }
            CreateCard(rowArray);
        }
    }

    private void CreateCard(string[] rowArray)
    {
        string cardType = rowArray[0];
        string cardID = rowArray[1];
        string cardName = rowArray[2];
        Card card = null;
        object[] args=null;
        if (!cardTypes.ContainsKey(cardID))
        {
            Debug.LogWarning($"读取卡牌配置文件时出现错误,未定义{cardID}的卡牌类");
            return;
        }
        if (cardType == "monster")
        {
            Debug.Assert(rowArray.Length >= 5, $"名为{cardID}的卡牌配置参数数目不匹配，期望>=5，实际{rowArray.Length}");
            int atk = int.Parse(rowArray[3]);
            int hp = int.Parse(rowArray[4]);
            string[] paras = rowArray.Skip(5).ToArray();
            args = new object[] { cardName, atk, hp, paras };
            card=CreateMonsterCard(cardID,args);
        }
        else if (cardType == "Spell")
        {
            Debug.Assert(rowArray.Length >= 3, $"名为{cardID}的卡牌配置参数数目不匹配，期望>=3，实际{rowArray.Length}");
            string[] paras = rowArray.Skip(3).ToArray();
            args = new object[] { cardName, paras };
            var ctor = cardTypes[cardName].GetConstructor(new Type[] { typeof(string), typeof(string[]) });
            card=CreateSpellCard(cardID,args);
        }
        AddCard(cardID, cardName, card,args);
    }

    private Card CreateMonsterCard(string cardID, object[] args)
    {
        var ctor = cardTypes[cardID].GetConstructor(new Type[] { typeof(string), typeof(int), typeof(int), typeof(string[]) });
        return CreateCard(ctor, args);
    }
    private Card CreateSpellCard(string cardID, object[] args)
    {
        var ctor = cardTypes[cardID].GetConstructor(new Type[] { typeof(string), typeof(string[]) });
        return CreateCard(ctor, args);
    }
    private Card CreateCard(ConstructorInfo ctor,object[] args)
    {
        var card = ctor.Invoke(args) as Card;
        var cardName = args[0] as string;
        if (card != null && !cardParams.ContainsKey(cardName)) cardParams.Add(cardName, args);
        return card;
    }
    private void AddCard(string id,string name,Card card,object[] args)
    {
        if (card == null)
        {
            Debug.LogError($"创建名称为{name},id为{id}的卡牌是失败，结果为null");
            return;
        }
        if (cardBox.ContainsKey(name))
        {
            Debug.LogError($"名称为{name},id为{id}的卡牌是定义重复");
            return;
        }
        cardBox.Add(name, card);
        cardNames.Add(name);
        cardParams.Add(name, args);
    }
    private static Dictionary<string,Type> GetAllCardTypes()
    {
        var res =new Dictionary<string, Type>();
        var cardType = typeof(Card);
        var assembly = cardType.Assembly;
        var assemblyAllTypes = assembly.GetTypes();
        foreach (var type in assemblyAllTypes)
        {
            if (!type.IsAbstract && type.IsSubclassOf(cardType))
            {
                res.Add(type.Name, type);
            }
        }
        return res;
    }
    public Card RandomCard()
    {
        var id = cardNames[UnityEngine.Random.Range(0, cardNames.Count)];
        return CopyCard(id);
    }
    public Card CopyCard(string name)
    {
        var type=cardBox[name].GetType();
        Card res=null;
        if (type.IsSubclassOf(typeof(MonsterCard)))
            res=CreateMonsterCard(type.Name, cardParams[name]);
        else if(type.IsSubclassOf(typeof(SpellCard)))
            res=CreateSpellCard(type.Name, cardParams[name]);
        if (res == null) Debug.LogError($"获取名为{name}的卡牌复制时出现错误");
        return res;
    }
}
