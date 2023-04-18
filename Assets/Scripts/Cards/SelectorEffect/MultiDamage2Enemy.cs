using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDamage2Enemy : CardEffect
{
    int damage;
    int damageTimes;
    int initTimes;
    public MultiDamage2Enemy(Card card,int damage,int damageTimes) : base(card)
    {
        this.damage = damage;
        this.damageTimes = damageTimes;
        initTimes=damageTimes;
    }

    public MultiDamage2Enemy(Card card,string[] paras) : base(card)
    {
        this.damage = int.Parse(paras[0]);
        this.damageTimes = int.Parse(paras[1]);
    }
    public override void CancleSelect()
    {
        Selections.Instance.canCancleSelect = true;
    }
    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
        Targets.Clear();
    }
    public override ISelector GetNextSelector()
    {
        Selections.Instance.canCancleSelect = false;
        var enemy = (Targets[0] as CardVisual).card;
        enemy.attacked.ApplyDamage(card,damage);
        damageTimes--;
        return this;
    }
    public override bool CanUse()
    {
        return damageTimes>0 && CardManager.Instance.Enemies.Has(e=>e.field.state==BattleState.Survive);
    }
    public override bool CanSelectTarget(ISeletableTarget target, int i)
    {
        if(target is CardVisual cardVisual)
        {
            var e = cardVisual.card;
            return e.field.state == BattleState.Survive;
        }
        return false;
    }
    public override void Excute()
    {

    }
    public override string ToString()
    {
        return $"对一名敌人造成{damage}点伤害，重复{initTimes}次";
    }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.EnemyCardOnBoard);
    }
}
