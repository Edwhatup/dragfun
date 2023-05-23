using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// 几何造物：破坏两个随从然后召唤一个具有他们复合能力的随从
/// </summary>
public class GeometricCreatureEffect : CardEffect
{
    int destroyCount;
    string summonUnit = "几何造物";
    private List<CardBuff> bufflist = new List<CardBuff>();
    private int atk = 0;
    private int hp = 0;

    public GeometricCreatureEffect(Card card, string[] paras) : base(card)
    {
        int.TryParse(paras[0], out destroyCount);
        summonUnit = paras[1];
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c => c.race == CardRace.Haniwa).Count >= destroyCount;
    }
    public GeometricCreatureEffect(Card card, int destroyCount, string summonUnit) : base(card)
    {
        this.destroyCount = destroyCount;
        this.summonUnit = summonUnit;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = destroyCount;
        for (int i = 0; i < destroyCount; i++)
        {
            CardTargets.Add(CardTarget.Monster);
        }

    }

    public override void Excute()
    {
        var info = new CardInfo() { name = summonUnit };
        foreach (CardVisual t in Targets)
        {
            var target = t.card;
            foreach (CardBuff buff in target.GetBuffList())
            {
                bufflist.Add(buff);
            }
            atk += target.attack.initAtk;
            hp += target.attacked.initMaxHp;
            target.attacked.Destroy(target);
        }
        Cell cell = CellManager.Instance.GetCells()
                                            .FindAll(c => c.CanSummon())
                                            .GetRandomItem();


        var geoCreture = CardStore.Instance.CreateCard(info);
        geoCreture.field.Summon(cell);
        foreach (var buff in bufflist)
        {
            geoCreture.AddBuff(buff);
        }
        geoCreture.AddBuff(new StatsPositiveBuff(atk, hp));

    }
    public override bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return (target as CardVisual).card.race == CardRace.Haniwa;
    }
    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }
    public override string ToString()
    {
        return $"破坏{destroyCount}个埴轮随从然后召唤一个融合的几何造物";
    }
}