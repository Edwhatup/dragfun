using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsDownCellHalo : ConstantCellEffect
{
    protected override List<Cell> Cells
        => CellManager.Instance.GetCells().FindAll(c =>
            StreetDistanceFromTriggered(c) == 1);
    protected override int LifeTime => -1;

    private int amount = 1;
    Cell cell;

    public StatsDownCellHalo(Card card, Cell cell, int amount) : base(card)
    {
        this.cell = cell;
        this.amount = amount;
    }

    public override void Execute(Card c)
    {
        c.attack.atk -= amount;
    }

    public override void Undo(Card c)
    {
        c.attacked.block += amount;
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
