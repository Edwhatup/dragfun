using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike:Card
{
    public Spike(CardInfo info) : base(info)
    {
        name = "尖刺";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponent(new AttackComponent(3) { pierce=1});
        AddComponent(new AttackedComponent(2));
        AddComponent(new ActionComponent());
        AddComponent(new SummonComponent());
        AddComponent(new UseComponent(1));
        GetDesc=() => "纵贯。";
    }
}
