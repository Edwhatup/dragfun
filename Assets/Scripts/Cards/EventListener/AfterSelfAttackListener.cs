using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterSelfAttackListener : EventListenerComponent
{
    public AfterSelfAttackListener(CardEffect effect) : base(effect)
    { }
    public override string ToString()
    {
        return $"攻击时," + effect.ToString();
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
        if (e is BeforeAttackEvent)
            Excute();
    }
    public override string ToString()
    {
        return $"在一名随从攻击前," + effect.ToString();
    }
}

public class AfterAttackListener : EventListenerComponent
{
    public AfterAttackListener(CardEffect effect) : base(effect) { }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterAttackEvent)
            Excute();
    }

    public override string ToString()
    {
        return $"在一名随从攻击后," + effect.ToString();
    }
}