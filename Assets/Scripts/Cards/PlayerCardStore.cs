using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using Card.Monster;
using Card.Spell;
namespace Card
{
    public class PlayerCardStore : MonoSingleton<PlayerCardStore>
    {
        public TextAsset cardData;
        public Dictionary<string, AbstractCard> cardBox = new Dictionary<string, AbstractCard>();
        private Dictionary<string, object[]> cardParams = new Dictionary<string, object[]>();
        private List<string> cardNames = new List<string>();
        private Dictionary<string, Type> cardTypes = new Dictionary<string, Type>();
        new void Awake()
        {
            base.Awake();
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
            AbstractCard card = null;
            object[] args = null;
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
                card = CreateMonsterCard(cardID, args);
            }
            else if (cardType == "spell")
            {
                Debug.Assert(rowArray.Length >= 3, $"名为{cardID}的卡牌配置参数数目不匹配，期望>=3，实际{rowArray.Length}");
                string[] paras = rowArray.Skip(3).ToArray();
                args = new object[] { cardName, paras };
                card = CreateSpellCard(cardID, args);
            }
            AddCard(cardID, cardName, card, args);
        }

        private AbstractCard CreateMonsterCard(string cardID, object[] args)
        {
            var ctor = cardTypes[cardID].GetConstructor(new Type[] { typeof(string), typeof(int), typeof(int), typeof(string[]) });
            return ctor.Invoke(args) as AbstractCard;
        }
        private AbstractCard CreateSpellCard(string cardID, object[] args)
        {
            var ctor = cardTypes[cardID].GetConstructor(new Type[] { typeof(string), typeof(string[]) });
            return ctor.Invoke(args) as AbstractCard;
        }
        private void AddCard(string id, string name, AbstractCard card, object[] args)
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
        private static Dictionary<string, Type> GetAllCardTypes()
        {
            var res = new Dictionary<string, Type>();
            var cardType = typeof(PlayerCard);
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
        public AbstractCard RandomCard()
        {
            var id = cardNames[UnityEngine.Random.Range(0, cardNames.Count)];
            return CopyCard(id);
        }
        public AbstractCard CopyCard(string name)
        {
            var type = cardBox[name].GetType();
            AbstractCard res = null;
            if (type.IsSubclassOf(typeof(MonsterCard)))
                res = CreateMonsterCard(type.Name, cardParams[name]);
            else if (type.IsSubclassOf(typeof(SpellCard)))
                res = CreateSpellCard(type.Name, cardParams[name]);
            if (res == null) Debug.LogError($"获取名为{name}的卡牌复制时出现错误");
            return res;
        }
    }
}