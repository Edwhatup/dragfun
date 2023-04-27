using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderHaniwa : Card
{
    public RiderHaniwa(CardInfo info) : base(info)
    {
        name = "骑士埴轮";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Haniwa;
        cost = 3;
        AddComponnet(new AttackComponent(4));
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new FieldComponnet());

        var e = new RequireRandomDraw(this,1,CardRace.Haniwa);
        AddComponnet(new SummonComponent(e));

        GetDesc = () => e.ToString();

        // 亡语还没写

    }
}
