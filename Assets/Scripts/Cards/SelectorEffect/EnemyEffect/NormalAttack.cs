using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : NoTargetCardEffect
{
    int damage; 
    public NormalAttack(Card card,int damage) : base(card)
    {
        this.damage = damage;
    }
    public override string ToString()
    {
        return $"对最近的目标攻击一次，造成{damage}点伤害";
    }
    public override void Excute()
    {
        var targets=CellManager.Instance.GetCells().FindAll(c=>c.row==0 && c.card!=null && c.card.camp==CardCamp.Friendly && c.card.attacked!=null);
        if (targets.Count > 0)
            targets[0].card.attacked.ApplyDamage(card, damage);
        else 
            Player.Instance.attacked.ApplyDamage(card, damage);
    }
}
