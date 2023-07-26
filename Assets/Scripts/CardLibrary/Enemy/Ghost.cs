﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Card
{
    public Ghost(CardInfo Info) : base(Info)
    {
        name = "怨灵";
        type = CardType.EnemyDerive;
        camp = CardCamp.Enemy;
        AddComponent(new AttackedComponent(5){ taunt = 1 });
        AddComponent(new FieldComponnet());
        AddComponent(new EnemyAction());
        AddComponent(new EnemyEffectListener(6, new EnemyNormalAttack(this, 3,1)) { priority = 0});
        enemyAction.GetNextAction();
        GetDesc = () => enemyAction.current?.ToString() ?? "";
    }
    //没做完
}   

