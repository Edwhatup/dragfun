using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BombMom : Card
{
    public BombMom(CardInfo Info) : base(Info)
    {
        name = "投嗣育母";
        type = CardType.Enemy;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(40));
        AddComponent(new EnemyAction());
        AddComponent(new DirectAtkCountdownComponent(5));
        AddComponent(new EnemyEffectListener(5, new EnemyNormalAttack(this, 3, 3)) { priority = 0, condition = () => GetComponent<AttackedComponent>().hp > GetComponent<AttackedComponent>().maxHp*0.5 });
        AddComponent(new EnemyEffectListener(5, new EnemyNormalAttack(this, 4, 4)) { priority = 0, condition = () => GetComponent<AttackedComponent>().hp <= GetComponent<AttackedComponent>().maxHp*0.5 });
        AddComponent(new EnemyEffectListener(7, new RandomSummonDerive(this, "爆炸小鬼", 2)) { priority = 1 });
        AddComponent(new EndTurnTriggerComponent(new BasicBuff(this,1,0)));
        var ls = GetComponnets<EnemyEffectListener>();
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }

}
public class BombChild : Card
{
    public BombChild(CardInfo Info) : base(Info)
    {
        name = "爆炸小鬼";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(5));
        AddComponent(new EnemyAction());
        AddComponent(new EnemyEffectListener(5, new RandomDamage2Enemy(this,3,RangeType.SmallCross,4)) { priority = 1 });
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";

        field.canSwap = 1;
    }
}