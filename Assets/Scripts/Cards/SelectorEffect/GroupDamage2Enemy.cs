
using System.Collections;
using System.Collections.Generic;

public class GroupDamage2Enemy : CardEffect
{
    int damage;
    public GroupDamage2Enemy(string[] paras) : base()
    {
        int.TryParse(paras[0], out damage);
    }
    public GroupDamage2Enemy(int damage) : base()
    {
        this.damage = damage;
    }

    public override string ToString()
    {
        return $"对所有敌人造成{damage}点伤害";
    }

    public override void Excute()
    {
        var enemies = CardManager.Instance.enemies;
        foreach (var enemy in enemies)
            if (enemy.field.state != BattleState.Dead)
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
