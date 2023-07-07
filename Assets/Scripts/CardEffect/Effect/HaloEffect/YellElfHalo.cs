using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellElfHalo : HaloEffect
{
    protected override List<Cell> Cells
        => CellManager.Instance.GetCells().FindAll(c => CellManager.Instance.GetStreetDistance(c, card.field.cell) !=0);
    private int extraDamage;
    CardBuff buff;

    public YellElfHalo(Card c, int extraDamage) : base(c)
    {
        this.extraDamage = extraDamage;
    }

    public override void InitTarget()
    {
        CardTargets.Add(CardTarget.Enemy);
    }

    public override void Execute(Card c)
    {
        buff =new StatsPositiveBuff(0,0,extraDamage);
        c.AddBuff(buff);
    }

    public override void Undo(Card c)
    {
        c.RemoveBuff(buff);
    }

    public override string ToString()
    {
        return $"全场的友方造成的任意伤害都增加{extraDamage}";
    }
}