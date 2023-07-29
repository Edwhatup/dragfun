using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDownCellHalo : ConstantCellEffect
{
    CardBuff buff;
    protected override List<Cell> Cells
        => CellManager.Instance.GetCells().FindAll(c =>
            StreetDistanceFromTriggered(c) == 0);
    public override int LifeTime => -1;

    private int amount = 1;
    Cell cell;

    public StatsDownCellHalo(Card card, Cell cell, int amount) : base(card)
    {
        this.cell = cell;
        this.amount = amount;
    }

    public override void Execute(Card c)
    {
        buff=new StatsDebuff(1,0);
        c.AddBuff(buff);
    }

    public override void Undo(Card c)
    {
        c.RemoveBuff(buff);
    }

    public override string ToString()
    {
        return $"降低其上随从{amount}点攻击力";
    }

    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.Monster);
    }
}
