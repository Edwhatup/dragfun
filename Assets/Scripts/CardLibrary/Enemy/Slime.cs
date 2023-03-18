using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Slime : Card
{
    public Slime(CardInfo Info) : base(Info)
    {
        name = "史莱姆";
        type=CardType.Enemy;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(40));
        AddComponnet(new EnemyAction());
        AddComponnet(new EnemyEffectListener(5, new EnemyNormalAttack(this, 3,3)) { priority = 0});
        AddComponnet(new EnemyEffectListener(7, new RandomSummonDerive(this, "小史莱姆",2)) { priority = 1 });
        var ls=GetComponnets<EnemyEffectListener>();
        enemyAction.GetNextAction();
        GetDesc =   ()=> enemyAction.current?.ToString()??"";
    }

}

public class SlimeChild : Card
{
    public SlimeChild(CardInfo Info) : base(Info)
    {
        name = "小史莱姆";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(5));
        AddComponnet(new EnemyAction());
        AddComponnet(new EnemyEffectListener(5, new EnemyNormalAttack(this, 3,3)) { priority = 0});
        AddComponnet(new DeadComponent(new GroupDebuff(this,2,2)));
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}