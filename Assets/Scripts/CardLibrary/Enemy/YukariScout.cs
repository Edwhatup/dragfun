using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class YukariScout : Card
{
    public YukariScout(CardInfo Info) : base(Info)
    {
        name = "八云紫的耳目";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(5));
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new EnemyEffectListener(5, new YukariScoutAttack(this, 3)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }

}