using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattery : Card
{
    public AutoBattery(CardInfo info):base(info)
    {
        name = "自动炮台";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Mech;
        cost = 1;
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new SummonComponent());
        var e=new AttackRandom(this);
        var r=new ResonanceComponent(e);
        AddComponnet(r);
        GetDesc = () => r.ToString();
    }
}