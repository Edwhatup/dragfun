﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombMom : Card
{
    public BombMom(CardInfo Info) : base(Info)
    {
        name = "投嗣育母";
        type=CardType.Enemy;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(40));
        AddComponnet(new EnemyAction());
        AddComponnet(new EnemyEffectListener(5, new EnemyNormalAttack(this, 3,3)) { priority = 0});
        AddComponnet(new EnemyEffectListener(7, new RandomSummonDerive(this, "爆炸小鬼",2)) { priority = 1 });
        var ls=GetComponnets<EnemyEffectListener>();
        enemyAction.GetNextAction();
        GetDesc =   ()=> enemyAction.current?.ToString()??"";
    }

}
public class BombChild : Card
{
    public BombChild(CardInfo Info) : base(Info)
    {
        name = "爆炸小鬼";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(5));
        AddComponnet(new EnemyAction());
        AddComponnet(new EnemyEffectListener(5, new GroupDamage1(this, 3)) { priority = 1 });
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}