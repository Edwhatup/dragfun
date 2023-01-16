using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在第一排随机位置召唤两个士兵
/// </summary>
public class RandomSummonOnFirstRow : NoTargetCardEffect
{
    int summonCount;
    string summonUnit;
    public RandomSummonOnFirstRow(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
    }
    public RandomSummonOnFirstRow(Card card, int summonCount,string summonUnit):base(card)
    {
        this.summonCount = summonCount;
        this.summonUnit = summonUnit;
        //Debug.Log("RS active");
    }

    public override void Excute()
    {
        Debug.Log("excute start");
        List<Cell> firstRollEmptyCell= new List<Cell> ();
        //foreach(Cell target in CellManager.Instance.GetCells())
        //{
        //    if((target as Cell).card==null&(target as Cell).row==0)
        //    {
        //        firstRollEmptyCell.Add(target);
        //    }

        //}
        //List<Cell> finalTargets = ListExtension.GetRandomItems(firstRollEmptyCell,summonCount);

        List<Cell> finalTargets = CellManager.Instance.GetCells()
                                            .FindAll(c => c.row == 0 && c.CanSummon())
                                            .GetRandomItems(summonCount);
        var info = new CardInfo()
        {
            name = summonUnit
        };
        foreach (Cell finalTarget in finalTargets)
        {
            var card = CardStore.Instance.CreateCard(info);
            card.field.Summon(finalTarget);
        }
    }
    public override string ToString()
    {
        return $"入场时，在第一排随机位置召唤{summonCount}个{summonUnit}";
    }
}