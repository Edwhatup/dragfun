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
        if (e is AfterSummonEvent)
        {
            var se = e as AfterSummonEvent;
            if (se.source == card)
            {
                if (se.handled)
                    Excute();
            }
            else
            {
                if (se.source.resonance != null)
                {
                    se.handled= true;   
                    Excute();
                }
            }
                
        }
    }

    public void Excute()
    {
        foreach (CardEffect effect in effects)
        {
            if (effect.CanUse())
                effect.Excute();
        }
    }

    public override string ToString()
    {
        string des="响应：";
        foreach(CardEffect effect in effects)
        {
            des=des+effect.ToString();
        }
        return des;
    }
}
