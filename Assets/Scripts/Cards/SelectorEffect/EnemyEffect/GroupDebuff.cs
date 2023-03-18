using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupDebuff : NoTargetCardEffect
{
    int hpmod;
    int atkmod;
    public GroupDebuff(Card card,int hpmod, int atkmod) : base(card)
    {
        this.hpmod=hpmod;
        this.atkmod=atkmod;
    }
    public override string ToString()
    {
        return $"使周围四格的敌方单位{-atkmod}/{-hpmod}";
    }
    public override void Excute()
    {
        var cell = card.field.cell;
        var cells = CellManager.Instance.GetCells().FindAll(c => CellManager.Instance.GetStreetDistance(c, cell)==1);
        foreach (var c in cells)
        {
            if (c.card != null && c.card.attacked != null && c.card.camp == CardCamp.Friendly)
                c.card.Buff(c.card,atkmod,hpmod);
        }
    }
}
