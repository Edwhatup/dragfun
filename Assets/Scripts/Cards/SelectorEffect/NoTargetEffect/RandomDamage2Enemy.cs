using System.Collections.Generic;
using UnityEngine;


public class RandomDamage2Enemy : NoTargetCardEffect
{
    int damage;
    public RandomDamage2Enemy(Card card, string[] paras) : base(card)
    {
        int.TryParse(paras[0], out damage);
    }
    public RandomDamage2Enemy(Card card, int damage) : base(card)
    {
        this.damage = damage;
    }

    public override string ToString()   
    {
        return $"随机对一名敌人造成{damage}点伤害";
    }

    public override void Excute()
    {
        Debug.Log("excute start");
        var enemy = CardManager.Instance.Enemies.FindAll(e=>e.field.state==BattleState.Survive).GetRandomItem();
        enemy.attacked.ApplyDamage(card, damage);
    }
}