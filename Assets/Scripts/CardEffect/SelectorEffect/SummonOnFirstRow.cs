using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在第一排指定位置召唤x个x
/// </summary>
public class SummonOnFirstRow : CardEffect
{
    int summonCount;
    string summonUnit;
    public SummonOnFirstRow(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
    }
    public override bool CanUse()
    {
        //return CardManager.Instance.board.FindAll(c => c.field.row == 1).Count!=CellManager.Instance.GetColCount();
        return CellManager.Instance.GetCells().FindAll(c => c.row == 0 && c.CanSummon()).Count >= summonCount;
    }
    public SummonOnFirstRow(Card card, int summonCount,string summonUnit):base(card)
    {
        this.summonCount = summonCount;
        this.summonUnit = summonUnit;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = summonCount;
        for(int i=0; i<summonCount; i++)
        {
            CardTargets.Add(CardTarget.Cell);
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
    }
    public override bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return(target as Cell).CanSummon() &(target as Cell).row==0;
    }
    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }
    public override string ToString()
    {
        //return $"入场时，在第一排召唤{summonCount}个{summonUnit}";
        return $"在第一排召唤{summonCount}个{summonUnit}";
    }
}