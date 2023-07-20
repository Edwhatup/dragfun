using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigElf : Card
{
    public BigElf(CardInfo Info) : base(Info)
    {
        name = "大妖精";
        type = CardType.Enemy;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(20){ });
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new DirectAtkCountdownComponent(5));
        AddComponent(new EnemyEffectListener(6, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        List<string> elfDervies= new List<string>{"庇护妖精","应援妖精","虚弱妖精"};
        AddComponent(new EnemyEffectListener(6, new RandomSummonDerive(this,elfDervies,2)) { priority = 1});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }

}
public class ShelterElf : Card
{
    public ShelterElf(CardInfo Info) : base(Info)
    {
        name = "庇护妖精";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(5){ taunt = 1 });
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new EnemyEffectListener(6, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }

}

public class YellElf : Card
{
    public YellElf(CardInfo Info) : base(Info)
    {
        name = "应援妖精";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(3));
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new HaloComponent(new YellElfHalo(this,1)));
        AddComponent(new EnemyEffectListener(5, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }

}

public class WeakElf : Card
{
    public WeakElf(CardInfo Info) : base(Info)
    {
        name = "虚弱妖精";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(3));
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new HaloComponent(new WeakHalo(this,2)));
        AddComponent(new EnemyEffectListener(5, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }

}
