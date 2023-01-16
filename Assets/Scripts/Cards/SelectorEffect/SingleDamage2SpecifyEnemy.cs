using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对指定敌人造成单次伤害
/// </summary>
public class SingleDamage2SpecifyEnemy : CardEffect
{
    int damage;
    public SingleDamage2SpecifyEnemy(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out damage);
    }
    public override bool CanUse()
    {
        return CardManager.Instance.Enemies.Has(e=>e.field.state==BattleState.Survive);    
    }
    public SingleDamage2SpecifyEnemy(Card card, int damage):base(card)
    {
        this.damage = damage;
    }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.Enemy | CardTarget.Monster);
    }
    public override void Excute()
    {
        var enemy =(Targets[0] as CardVisual).card;
        enemy.attacked.ApplyDamage(card, damage);
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return $"对一名造成单位{damage}点伤害";
    }
}