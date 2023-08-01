using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAttack : NoTargetCardEffect
{
    int damage;
    int count;
    public EnemyNormalAttack(Card card, int damage, int count) : base(card)
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
        var cdn = card.GetComponent<DirectAtkCountdownComponent>();
        if(card.field.row==-1)
        {
            if (cdn != null && cdn.Ready) Face(0);
            else NoFace(0);
        }
        else if(card.field.row==5)
        {
            if (cdn != null && cdn.Ready) Face(4);
            else NoFace(4);
        }
        else Debug.Log("敌人主体位置错误！");
        
    }

    private void NoFace(int row)
    {
        for (int i = 0; i < count; i++)
        {
            var targets = CellManager.Instance.GetCells()
                    .FindAll(c => c.row == row &&
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

    private void Face(int row)
    {
        for (int i = 0; i < count - 1; i++)
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
