using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopPool
{
    private const string DATA_PATH = "Datas/ShopData";
    private const string RULE_PATH = "Datas/ShopRollRule";

    private List<Card> cards = new List<Card>();
    private Dictionary<int, Dictionary<CardRarity, float>> rules = new Dictionary<int, Dictionary<CardRarity, float>>();

    public int Count => cards.Count;

    public ShopPool()
    {
        var data = Resources.Load<TextAsset>(DATA_PATH);
        if (!data)
        {
            Debug.LogError($"商店的配置文件在 {DATA_PATH} 处未找到，是否移动过？");
            return;
        }

        var lines = data.text.Split('\n');
        foreach (var item in lines)
        {
            var result = item.Split(':');
            if (result.Length == 2)
            {
                if (int.TryParse(result[1], out var val))
                    for (int i = 0; i < val; i++) cards.Add(CardStore.Instance.CreateCard(result[0], false));
                else Debug.LogError($"商店配置: {result[0]} 需要指定一个数字作为数量");
            }
            // else Debug.LogError($"商店配置: 需要 \"卡牌名:数量\" 格式！");
        }

        data = Resources.Load<TextAsset>(RULE_PATH);
        if (!data)
        {
            Debug.LogError($"商店稀有度的配置文件在 {RULE_PATH} 处未找到，是否移动过？");
            return;
        }

        lines = data.text.Split('\n');
        for (int i = 1; i < lines.Length; i++)  // 略去第一行的示例
        {
            var args = lines[i].Split(',');
            if (int.TryParse(args[0], out var shopLvl))
            {
                var dict = new Dictionary<CardRarity, float>();
                for (int j = 1; j < args.Length; j++)
                {
                    var pair = args[j].Split(':');
                    if (int.TryParse(pair[0], out var rarity))
                    { dict.Add((CardRarity)rarity, float.Parse(pair[1])); }
                    else Debug.LogError($"商店规则配置第{i + 1}行: 卡牌稀有度应当是一个整数!");

                    if (rules.ContainsKey(shopLvl)) rules[shopLvl] = dict;
                    else rules.Add(shopLvl, dict);
                }
            }
            else Debug.LogError($"商店规则配置第{i + 1}行: 商店的等级应当是一个整数!");
        }
    }

    public List<Card> FetchRandom(int shopLvl, int count)
    {
        List<Card> results = new List<Card>();

        if (!rules.ContainsKey(shopLvl))
        {
            Debug.LogError($"{shopLvl}等级的商店没有出现在配置文件中");
            return null;
        }

        var rule = rules[shopLvl];

        for (int i = 0; i < count; i++)
        {
            var ras = rule.Keys.ToArray();
            foreach (var item in ras)
                if (cards.FindAll(e => e.rarity == item).Count == 0) rule.Remove(item);

            var totalWeight = rule.Sum(j => j.Value);
            if (totalWeight == 0) break;

            var rand = UnityEngine.Random.Range(0, totalWeight);
            CardRarity targetRar = CardRarity.All;
            foreach (var item in rule)
            {
                rand -= item.Value;
                if (rand <= 0) { targetRar = item.Key; break; }
            }

            var targets = targetRar == CardRarity.All ? cards : cards.FindAll(k => k.rarity == targetRar);
            // if (targets.Count == 0) continue;

            var selected = targets[UnityEngine.Random.Range(0, targets.Count)];
            cards.Remove(selected);
            results.Add(selected);
        }
        return results;
    }
}