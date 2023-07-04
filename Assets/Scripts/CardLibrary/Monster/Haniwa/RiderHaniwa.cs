﻿using System.Collections;
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
        cost = 2;
        AddComponent(new AttackComponent(4));
        AddComponent(new AttackedComponent(1));
        AddComponent(new ActionComponent());
        AddComponent(new FieldComponnet());

        var e = new RequireRandomDraw(this,1,CardRace.Haniwa);
        AddComponent(new SummonComponent(e));


        var d = new RandomDamage2Enemy(this,4,RangeType.Round,1);
        AddComponent(new DeadComponent(d));

        GetDesc = () => e.ToString()+"死亡时："+d.ToString();

    }
}
