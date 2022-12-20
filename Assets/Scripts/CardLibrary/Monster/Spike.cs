using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike:Card
{
    public Spike()
    {
        name = "尖刺";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackComponent(3) { pierce=1});
        AddComponnet(new AttackedComponent(2));
        AddComponnet(new FieldComponnet());
        AddComponnet(new UseComponent(new SummonComponent(), 1));
        GetDesc=() => "纵贯。";
    }
}
