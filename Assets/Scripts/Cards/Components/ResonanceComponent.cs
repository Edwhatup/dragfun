using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CanRepeat(true)]
[RequireCardComponent(typeof(FieldComponnet))]
public class ResonanceComponent : EventListenerComponent
{
    public ResonanceComponent(NoTargetCardEffect effect):base(effect){}
    
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
                if (se.source.resonance != null && se.source.field!=null)
                {
                    var field = se.source.field;
                    if(field.row==card.field.row || field.col==card.field.col)
                    {
                        se.handled = true;
                        Excute();
                    }
                }
            }
                
        }
    }

    public override string ToString()
    {
        return "呼应," + effect.ToString();
        throw new System.NotImplementedException();
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
