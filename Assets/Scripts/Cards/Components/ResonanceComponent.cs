using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CanRepeat(false)]
[RequireCardComponent(typeof(FieldComponnet))]
public class ResonanceComponent : EventListenerComponent
{
    List<CardEffect> effects;
    public ResonanceComponent(CardEffect effect)
    {
        effects = new List<CardEffect>() { effect };
    }
    public ResonanceComponent(IEnumerable<CardEffect> effects)
    {
        effects = new List<CardEffect>(effects);
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if(e is AfterSummonEvent)
        {
            var se=e as AfterSummonEvent;
            if(se.source.resonance!=null)
            {

                Excute();
            }            
        }
    }

    public void Excute()
    {
        foreach(CardEffect effect in effects)
        {
            if (effect.CanUse())
                effect.Excute();
        }
    }
}
