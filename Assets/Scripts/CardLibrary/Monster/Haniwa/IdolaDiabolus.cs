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
        cost = 3;
        AddComponent(new AttackComponent(6));
        AddComponent(new AttackedComponent(6));
        AddComponent(new ActionComponent());
        AddComponent(new FieldComponnet());

        var e = new RequireRandomDraw(this,1,CardRace.Haniwa);
        AddComponent(new SummonComponent(e));


        var d = new RandomDamage2Enemy(this,4,RangeType.Round,1);
        AddComponent(new DeadComponent(d));

        GetDesc = () => e.ToString()+"死亡时："+d.ToString();

    }
}
