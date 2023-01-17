using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAttackListener : EventListenerComponent
{
    public SelfAttackListener(CardEffect effect) : base(effect)
    { }
    public override string ToString()
    {
        return $"攻击后," + effect.ToString();
    }
    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterAttackEvent && e.source == card)
            Excute();
    }
}

public class BeforeAttackListener:EventListenerComponent
{
    public BeforeAttackListener(CardEffect effect) : base(effect) { }
    public override void EventListen(AbstractCardEvent e)
    {
        if (e is BeforeAttackEvent && e.source == card)
            Excute();
    }
    public override string ToString()
    {
        return $"在攻击时," + effect.ToString();
    }
}
