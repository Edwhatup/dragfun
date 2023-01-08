using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在第一排随机位置召唤两个士兵
/// </summary>
public class RandomSummonOnFirstRow : CardEffect
{
    int summonCount;
    string summonUnit;
    public RandomSummonOnFirstRow(string[] paras)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
        InitTarget();
    }
    public RandomSummonOnFirstRow(int summonCount,string summonUnit)
    {
        this.summonCount = summonCount;
        this.summonUnit = summonUnit;
        //Debug.Log("RS active");
        InitTarget();
    }
    public override void InitTarget()
    {
        NoTarget();
    }

    public override void Excute()
    {
        Debug.Log("excute start");
        List<Cell> firstRollEmptyCell= new List<Cell> ();
        foreach(Cell target in CellManager.Instance.GetCells())
        {
            if((target as Cell).card==null&(target as Cell).row==0)
            {
                firstRollEmptyCell.Add(target);
            }
            
        }
        List<Cell> finalTargets = ListExtension.GetRandomItems(firstRollEmptyCell,summonCount);
        foreach(Cell finalTarget in finalTargets)
        {
            var card = CardStore.Instance.CreateCard(summonUnit);
            card.field.Summon(finalTarget);
        }
    }

    public override void OnSelected()
    {
        return;
    }

    public override string ToString()
    {
        return $"入场时，在第一排随机位置召唤{summonCount}个{summonUnit}";
    }
}