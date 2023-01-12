using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterSelfAttackListener : EventListenerComponent
{
    CardEffect effect;

    public AfterSelfAttackListener(CardEffect effect)
    {
        this.effect = effect;
    }
    public override string ToString()
    {
        return $"攻击时," + effect.ToString();
    }
    public override void EventListen(AbstractCardEvent e)
    {
        if(e is AfterAttackEvent && e.source==card)
        {
            effect.Excute();
        }
    }
}
