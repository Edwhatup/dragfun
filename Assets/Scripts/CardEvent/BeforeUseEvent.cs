using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeforeUseEvent : AbstractCardEvent
{
    public BeforeUseEvent(Card source) : base(source, null, 0)    {

    }
}
public class AfterUseEvent : AbstractCardEvent
{
    public AfterUseEvent(Card source, int cost) : base(source, cost)
    {

    }
}