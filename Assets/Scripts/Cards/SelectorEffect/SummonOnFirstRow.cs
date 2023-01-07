using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在第一排随机位置召唤两个士兵
/// </summary>
public class SummonOnFirstRow : CardEffect
{
    int summonCount;
    string summonUnit;
    public SummonOnFirstRow(string[] paras)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
        InitTarget();
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c => c.field.row == 1).Count!=CellManager.Instance.GetColCount();
    }
    public SummonOnFirstRow(int summonCount,string summonUnit)
    {
        this.summonCount = summonCount;
        this.summonUnit = summonUnit;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = summonCount;
        var targets = new List<CardTarget>();
        for(int i=0; i<summonCount; i++)
        {
            targets.Add(CardTarget.Cell);
        }
        CardTargets = targets;
    }

    public override void Excute()
    {
        foreach(Cell target in Targets)
        {
            var card = CardStore.Instance.CreateCard(summonUnit);
            card.field.Summon(target);
        }
    }
    public override bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return(target as Cell).card==null&(target as Cell).row==1;
    }
    

    public override void OnSelected()
    {
        return;
    }

    public override string ToString()
    {
        return $"在第一排随机位置召唤{summonCount}个{summonUnit}";
    }
}