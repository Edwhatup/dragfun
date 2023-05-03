using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBuffEvent : AbstractCardEvent
{
    public CardBuff buff;

    public AfterBuffEvent(Card source, Card target) : base(source, target, 0) { }
    public AfterBuffEvent(Card source, Card target, CardBuff buff) : base(source, target, 0) { this.buff = buff; }
}
