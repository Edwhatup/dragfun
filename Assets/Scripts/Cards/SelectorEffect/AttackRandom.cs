using System;
using System.Collections;
using System.Collections.Generic;

public class AttackRandom : CardEffect
{
    int damage;
    RangeType rangeType;
    public AttackRandom(string[] paras) : base()
    {
        return;
    }
    public AttackRandom() : base()
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
            var enemies = CardManager.Instance.enemies.FindAll(e => e.attacked.GetAttackDistance(card) <= card.attack.AtkRange);
            Card target= enemies.GetRandomItem();
            card.attack.Attack(target,false);
        }
        
            
    }

    public override void InitTarget()
    {
        NoTarget();
    }

    public override void OnSelected()
    {
        return;
    }
}
