using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEvent : AbstractCardEvent
{
    public BuffEvent(Card source, Card target) : base(source, target, 0)
    {
    }
}
