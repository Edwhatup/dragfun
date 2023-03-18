using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shaman : Card
{
    public Shaman(CardInfo Info) : base(Info)
    {
        name = "萨满";
        type=CardType.Enemy;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(40));
        AddComponnet(new EnemyAction());
        AddComponnet(new EnemyEffectListener(5, new EnemyNormalAttack(this, 3,3)) { priority = 0});
        AddComponnet(new EnemyEffectListener(7, new BuffAtkByDerNumber(this,3)){ priority = 1 });
        var ls=GetComponnets<EnemyEffectListener>();
        enemyAction.GetNextAction();
        GetDesc =   ()=> enemyAction.current?.ToString()??"";
    }

}
public class SteelElement : Card
{
    public SteelElement(CardInfo Info) : base(Info)
    {
        name = "钢铁元素";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(5){taunt = 1});
        AddComponnet(new EnemyAction());
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}

public class MiasmaElement : Card
{
    public MiasmaElement(CardInfo Info) : base(Info)
    {
        name = "瘴气元素";
        //没做完，应该是一个2x2的易伤光环。
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(5){});
        AddComponnet(new EnemyAction());
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}

public class AcidElement : Card
{
    public AcidElement(CardInfo Info) : base(Info)
    {
        name = "强酸元素";
        //没做完，应该是一个2x2的虚弱光环。
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(5){});
        AddComponnet(new EnemyAction());
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}

public class OriginElement : Card
{
    public OriginElement(CardInfo Info) : base(Info)
    {
        name = "原始元素";
        //没做完，应该是一个全场敌人的加结算伤害光环。
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponnet(new AttackedComponent(2){});
        AddComponnet(new EnemyAction());
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}
