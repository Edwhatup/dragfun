using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在第n排随机位置召唤两个随从单位
/// </summary>
public class RandomSummonOnSpecificRow : NoTargetCardEffect
{
    int summonCount;
    string summonUnit;
    int row;
    public RandomSummonOnSpecificRow(Card card,string[] paras):base(card)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
        int.TryParse(paras[0], out row);
    }
    public RandomSummonOnSpecificRow(Card card, int summonCount,string summonUnit,int row):base(card)
    {
        this.summonCount = summonCount;
        this.summonUnit = summonUnit;
        this.row=row;
    }

    public override void Excute()
    {
        //Debug.Log("excute start");
        List<Cell> firstRollEmptyCell= new List<Cell> ();

        List<Cell> finalTargets = CellManager.Instance.GetCells()
                                            .FindAll(c => c.row == row && c.CanSummon())
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
        return $"入场时，在第{row+1}排随机位置召唤{summonCount}个{summonUnit}";
    }
}