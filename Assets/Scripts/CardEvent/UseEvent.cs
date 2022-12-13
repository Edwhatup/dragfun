using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseEvent : AbstractCardEvent
{
    public UseEvent(Card source,int ppCost) : base(source, null, ppCost)    {

    }
}
