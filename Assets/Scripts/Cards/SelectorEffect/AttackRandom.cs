using System;
using System.Collections;
using System.Collections.Generic;

public class AttackRandom : CardEffect
{
    int damage;
    RangeType rangeType;
    public AttackRandom(string[] paras) : base()
    {
        int.TryParse(paras[0], out damage);
        Enum.TryParse(paras[1],false,out RangeType rangeType);
    }
    public AttackRandom(int damage,RangeType rangeType) : base()
    {
        this.damage = damage;
        this.rangeType = rangeType;
    }

    public override string ToString()
    {
        return $"攻击一次随机敌方目标";
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
