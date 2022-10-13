using Core.GameEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackAction:AbstractAction
{
    public BattleCard[] targets;
    public int damage;
    
    public AttackAction(BattleCard soure, BattleCard target,int damage) : base(soure)
    {
        this.targets = new BattleCard[] { target };
        this.damage = damage;
        this.actionTarget = ActionTarget.Specify;
    }
    public AttackAction(BattleCard soure, BattleCard[] targets, int damage) : base(soure)
    {
        this.targets = targets;
        this.damage = damage;
        this.actionTarget = ActionTarget.Specify;
    }
    public override void Execute()
    {
        GameEventSystem.CallInterfaceMethod<IOnAttack>(sourse, this);
        switch (this.actionTarget)
        {
            case ActionTarget.Specify:
                //targets[0].ApplyDamage(damage);
                break;
        }

    }
}
