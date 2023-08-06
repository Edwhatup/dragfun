using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Komachi : Card
{
    public Komachi(CardInfo Info) : base(Info)
    {
        name = "小野塚小町";
        type = CardType.Enemy;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(40){});
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new DirectAtkCountdownComponent(20));
        AddComponent(new EnemyEffectListener(7, new RandomSummonDerive(this, "怨灵", 2)) { priority = 1 });
        AddComponent(new EnemyEffectListener(6, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}   

public class Ghost : Card
{
    public Ghost(CardInfo Info) : base(Info)
    {
        name = "怨灵";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(5){});
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new DeadComponent(new StatsDownCellHalo(this,this.field.cell,1)));
        AddComponent(new EnemyEffectListener(6, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}

public class EvilSpirit : Card
{
    public EvilSpirit(CardInfo Info) : base(Info)
    {
        name = "恶灵";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(5){});
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new DeadComponent(new PlayerDamage(this,3)));
        AddComponent(new EnemyEffectListener(6, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
}




