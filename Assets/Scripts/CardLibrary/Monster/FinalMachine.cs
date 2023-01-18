using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMachine:Card
{
    public FinalMachine(CardInfo info) : base(info)
    {
        name = "最终兵器";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Mech;
        cost = 3;
        AddComponnet(new AttackComponent(5) {atkRange=2, sweep=1,pierce=1});
        AddComponnet(new AttackedComponent(5));
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        GetDesc=() => "增程1，横扫，纵贯";
    }
}
