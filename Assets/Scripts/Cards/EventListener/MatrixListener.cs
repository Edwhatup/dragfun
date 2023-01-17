using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixListener : EventListenerComponent
{
    public MatrixListener()
    { }
    public override void EventListen(AbstractCardEvent e)
    {
        if (e is BeforeAttackEvent && e.source.field.col==card.field.col && e.source.field.row>card.field.row && e.target.field.row==-1)
            e.source.attack.extraDamageRate+=0.5;
    }
    public override string ToString()
    {
        return $"在其后的随从攻击敌人主体时额外造成50%伤害";
    }
}

