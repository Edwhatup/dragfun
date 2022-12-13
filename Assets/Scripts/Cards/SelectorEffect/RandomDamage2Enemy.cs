using System.Collections.Generic;

public class RandomDamage2Enemy : CardEffect
{
    int damage;
    public RandomDamage2Enemy(string[] paras) : base()
    {
        int.TryParse(paras[0], out damage);
    }
    public RandomDamage2Enemy(int damage) : base()
    {
        this.damage = damage;
    }

    public override string Desc()
    {
        return $"随机对一名敌人造成{damage}点伤害";
    }

    public override void Excute()
    {
        var enemy = EnemyManager.Instance.GetRandomEnemy();
        enemy.attacked.ApplyDamage(card, damage);
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