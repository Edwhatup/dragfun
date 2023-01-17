using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupDamage1 : NoTargetCardEffect
{
    int damage;
    public GroupDamage1(Card card,int damage) : base(card)
    {
        this.damage = damage;
    }
    public override string ToString()
    {
        return $"对周围四格的右方单位造成{damage}点伤害";
    }
    public override void Excute()
    {
        var cell = card.field.cell;
        var cells = CellManager.Instance.GetCells().FindAll(c => CellManager.Instance.GetStreetDistance(c, cell)==1);
        foreach (var c in cells)
        {
            if (c.card != null && c.card.attacked != null && c.card.camp == CardCamp.Friendly)
                c.card.attacked.ApplyDamage(card,damage);
        }
    }
}
