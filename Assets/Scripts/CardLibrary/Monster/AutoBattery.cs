using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattery : Card
{
    public AutoBattery()
    {
        name = "自动炮台";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Mech;
        AddComponnet(new AttackComponent(4));
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new UseComponent(new SummonComponent(this), 2));
        var e=new AttackRandom();
        var r=new ResonanceComponent(e);
        AddComponnet(r);
        GetDesc = () => r.ToString();
    }
}