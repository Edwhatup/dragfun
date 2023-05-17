using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 几何造物：破坏两个随从然后召唤一个具有他们复合能力的随从
/// </summary>
public class GeometricCreatureEffect : CardEffect
{
    int summonCount;
    int destroyCount;
    string summonUnit;
    public GeometricCreatureEffect(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
    }
    public override bool CanUse()
    {
        return CellManager.Instance.GetCells().FindAll(c => c.card.race==CardRace.Haniwa).Count >= destroyCount;
    }
    public GeometricCreatureEffect(Card card, int summonCount,string summonUnit):base(card)
    {
        this.summonCount = summonCount;
        this.summonUnit = summonUnit;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = summonCount;
        for (int i=0; i<summonCount; i++)
        {
            CardTargets.Add(CardTarget.Monster);
        }
        
    }

    public override void Excute()
    {
        var info = new CardInfo() { name = summonUnit };
        foreach(Cell target in Targets)
        {
            var card = CardStore.Instance.CreateCard(info);
            card.field.Summon(target);
        }
        //完全没做完
    }
    public override bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return(target as Cell).card.race==CardRace.Haniwa;
    }
    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }
    public override string ToString()
    {
        return $"破坏{summonCount}个埴轮随从然后召唤一个具有他们所有能力的几何造物";
    }
}