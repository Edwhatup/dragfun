using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattery : Card
{
    public AutoBattery(CardInfo info) : base(info)
    {
        name = "自动炮台";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Mech;
        cost = 1;
        AddComponent(new AttackedComponent(1));
        AddComponent(new SummonComponent());
        var e = new AttackRandom(this);
        var r = new ResonanceComponent(e);
        AddComponent(r);
        AddComponent(new ShopActionComponent(
                new ShopSingleBuffEffect(
                    new StatsPositiveBuff(1, 1))));
        GetDesc = () => r.ToString();
    }
}