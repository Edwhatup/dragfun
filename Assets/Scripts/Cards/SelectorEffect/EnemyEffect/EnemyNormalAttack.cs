using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAttack : NoTargetCardEffect
{
    int damage; 
    int count;
    public EnemyNormalAttack(Card card,int damage,int count) : base(card)
    {
        this.damage = damage;
        this.count = count;
    }
    public override string ToString()
    {
        return $"进行{damage}x{count}次攻击";
    }
    public override void Excute()
    {
        for(int i=0 ; i<count ;i++)
        {
            var targets=CellManager.Instance.GetCells().FindAll(c=>c.row==0 && c.card!=null && c.card.camp==CardCamp.Friendly && c.card.attacked!=null);
            if (targets.Count > 0)
                targets[0].card.attacked.ApplyDamage(card, damage);
            else 
                Player.Instance.attacked.ApplyDamage(card, damage);
        }
        
    }
}
