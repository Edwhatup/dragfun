using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : NoTargetCardEffect
{
    int damage;
    public PlayerDamage(Card card,int damage) : base(card)
    {
        this.damage = damage;
    }
    public override string ToString()
    {
        return $"对你造成{damage}点伤害";
    }
    public override void Excute()
    {
        Player.Instance.attacked.ApplyDamage(card, damage);
    }
}
