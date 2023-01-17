using System;
using System.Collections;
using System.Collections.Generic;

public class AllExtarRangeAttack : NoTargetCardEffect
{
    int damage;
    RangeType rangeType;
    public AllExtarRangeAttack(Card card, string[] paras) : base(card)
    {
        return;
    }
    public AllExtarRangeAttack(Card card) : base(card)
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
            var extraRangeMonsters = CardManager.Instance.FriendlyMonsterOnBorad.FindAll(e =>
                    e.attack.atkRange>1);
            foreach(var monster in extraRangeMonsters)
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
                    monster.attack.Attack(target,false);
                }               
            }
        }
    }
}

