using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigElfSummon : NoTargetCardEffect
{
    int damage;
    int count;
    public BigElfSummon(Card card, int damage, int count) : base(card)
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
        if(card.GetComponent<DirectAtkCountdownComponent>().Ready!=true)
        {
            NoFace();
        }
        else Face();

    }

    private void NoFace()
    {
        for (int i = 0; i < count; i++)
        {
            var targets = CellManager.Instance.GetCells()
                    .FindAll(c => c.row == 0 &&
                                    c.card != null &&
                                    c.card.camp == CardCamp.Friendly &&
                                    c.card.attacked != null &&
                                    c.card.field.state == BattleState.Survive);
            if (targets.Count > 0)
            {
                targets[0].card.attacked.ApplyDamage(card, damage);
            }
            else
                Player.Instance.attacked.ApplyDamage(card, damage);
        }
    }

    private void Face()
    {
        for (int i = 0; i < count-1; i++)
        {
            var targets = CellManager.Instance.GetCells()
                    .FindAll(c => c.row == 0 &&
                                    c.card != null &&
                                    c.card.camp == CardCamp.Friendly &&
                                    c.card.attacked != null &&
                                    c.card.field.state == BattleState.Survive);
            if (targets.Count > 0)
            {
                targets[0].card.attacked.ApplyDamage(card, damage);
            }
            else
                Player.Instance.attacked.ApplyDamage(card, damage);
        }
        Player.Instance.attacked.ApplyDamage(card, damage);
    }
}
