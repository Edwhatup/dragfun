using System.Collections.Generic;
/// <summary>
/// 对指定敌人造成单次伤害
/// </summary>
public class SingleDamage2SpecifyEnemy : CardEffect
{
    int damage;
    public SingleDamage2SpecifyEnemy(string[] paras)
    {
        int.TryParse(paras[0], out damage);
        InitTarget();
    }
    public override bool CanUse()
    {
        return CardManager.Instance.enemies.Has(e=>e.field.state==BattleState.Survive);    
    }
    public SingleDamage2SpecifyEnemy(int damage)
    {
        this.damage = damage;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets = new List<CardTarget>() { CardTarget.Enemy };
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
        return $"对一名敌人造成{damage}点伤害";
    }
}