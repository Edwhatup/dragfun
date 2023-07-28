using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWeakHalo : HaloEffect
{
    protected override List<Cell> Cells
        => CellManager.Instance.GetCells().FindAll(c => CellManager.Instance.GetStreetDistance(c, card.field.cell) ==0);
    private int atk;
    CardBuff buff;

    public GhostWeakHalo(Card c, int atk) : base(c)
    {
        this.atk = atk;
    }

    public override void InitTarget()
    {
        CardTargets.Add(CardTarget.Enemy);
    }

    public override void Execute(Card c)
    {
        buff =new StatsDebuff(atk,0,0);
        c.AddBuff(buff);
    }

    public override void Undo(Card c)
    {
        c.RemoveBuff(buff);
    }

    public override string ToString()
    {
        return $"该格的敌人造成的攻击减少{atk}";
    }
}
