﻿using System;
using System.Collections;
using System.Collections.Generic;

public class GroupDamage : CardEffect
{
    int damage;
    RangeType rangeType;
    public GroupDamage(string[] paras) : base()
    {
        int.TryParse(paras[0], out damage);
        Enum.TryParse(paras[1],false,out RangeType rangeType);
    }
    public GroupDamage(int damage,RangeType rangeType) : base()
    {
        this.damage = damage;
        this.rangeType = rangeType;
    }

    public override string ToString()
    {
        return $"对所有敌人造成{damage}点伤害";
    }

    public override void Excute()
    {
        if(rangeType==RangeType.AllEnemies)
        {
            var enemies = CardManager.Instance.onBoardEnemies;
            foreach (var enemy in enemies)
                if (enemy.field.state != BattleState.Dead)
                    enemy.attacked.ApplyDamage(card, damage);
        }
        if(rangeType==RangeType.AllEnemiesOnBoard)
        {
            var enemies = CardManager.Instance.onBoardEnemies;
            foreach (var enemy in enemies)
            if (enemy.field.state != BattleState.Dead)
                enemy.attacked.ApplyDamage(card, damage);
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
