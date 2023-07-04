using System.Collections.Generic;
using UnityEngine;


public class RandomDamage2Enemy : NoTargetCardEffect
{
    int damage;
    RangeType range = RangeType.AllEnemies;
    int targetCnt = 1;

    public RandomDamage2Enemy(Card card, string[] paras) : base(card)
    {
        int.TryParse(paras[0], out damage);
    }
    public RandomDamage2Enemy(Card card, int damage) : base(card)
    {
        this.damage = damage;
    }

    public RandomDamage2Enemy(Card card, int damage, RangeType range, int targetCnt) : base(card)
    {
        this.range = range;
        this.targetCnt = targetCnt;
        this.damage = damage;
    }

    public override string ToString()
    {
        if (range == RangeType.AllEnemies && targetCnt == 1)
        { return $"随机对一名敌人造成{damage}点伤害"; }
        else
            return $"随机对{CardManager.GetSpecificAreaName(range)}的{targetCnt}名敌人造成{damage}点伤害";
    }

    public override void Excute()
    {
        //Debug.Log("excute start");
        if (range == RangeType.AllEnemies && targetCnt == 1)
        {
            var enemy = CardManager.Instance.Enemies.FindAll(e => e.field.state == BattleState.Survive).GetRandomItem();
            enemy.attacked.ApplyDamage(card, damage);
        }
        else
        {
            var enemy = CardManager.Instance.GetSpecificAreaEnemies(card, card, range).GetRandomItems(targetCnt);
            foreach (var a in enemy)
            {
                a.attacked.ApplyDamage(card, damage);
            }
        }
    }
}