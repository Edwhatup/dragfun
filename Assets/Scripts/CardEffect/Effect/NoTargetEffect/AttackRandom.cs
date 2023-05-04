using System;
using System.Collections;
using System.Collections.Generic;

public class AttackRandom : NoTargetCardEffect
{
    int damage;
    RangeType rangeType;
    public AttackRandom(Card card, string[] paras) : base(card)
    {
        return;
    }
    public AttackRandom(Card card) : base(card)
    {
        return;
    }

    public override string ToString()
    {
        return $"随机攻击一次";
    }

    public override void Excute()
    {
        if(card.action.GetCanAttack()==false)
        {
            return;
        }
        else
        {
            //var enemies = CardManager.Instance.enemies.FindAll(e => e.attacked.GetAttackDistance(card) <= card.attack.AtkRange);
            var enemies = CardManager.Instance.Enemies.FindAll(e =>
                    e.field.state==BattleState.Survive && 
                    e.attacked.GetAttackDistance(card) <= card.attack.AtkRange);
            Card target = enemies.GetRandomItem();
            card.attack.Attack(target, false);
        }
    }
}
