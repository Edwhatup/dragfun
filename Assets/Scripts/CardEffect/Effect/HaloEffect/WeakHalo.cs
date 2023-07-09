using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakHalo : HaloEffect
{
    protected override List<Cell> Cells
        => CellManager.Instance.GetCells().FindAll(c => CellManager.Instance.GetStreetDistance(c, card.field.cell) ==1);
    private int extraDamage;
    CardBuff buff;

    public WeakHalo(Card c, int extraDamage) : base(c)
    {
        this.extraDamage = extraDamage;
    }

    public override void InitTarget()
    {
        CardTargets.Add(CardTarget.Enemy);
    }

    public override void Execute(Card c)
    {
        buff =new StatsDebuff(0,0,extraDamage);
        c.AddBuff(buff);
    }

    public override void Undo(Card c)
    {
        c.RemoveBuff(buff);
    }

    public override string ToString()
    {
        return $"周围一格内的敌人造成的伤害减少{extraDamage}";
    }
}
