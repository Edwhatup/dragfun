using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolaDiabolus : Card
{
    public IdolaDiabolus(CardInfo info) : base(info)
    {
        name = "造型恶魔";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Haniwa;
        cost = 6;
        AddComponnet(new AttackComponent(6));
        AddComponnet(new AttackedComponent(6));
        AddComponnet(new ActionComponent());
        AddComponnet(new FieldComponnet());

        var e = new RequireRandomDraw(this,1,CardRace.Haniwa);
        AddComponnet(new SummonComponent(e));


        var d = new RandomDamage2Enemy(this,4,RangeType.Round,1);
        AddComponnet(new DeadComponent(d));

        GetDesc = () => e.ToString()+"死亡时："+d.ToString();

    }
}
