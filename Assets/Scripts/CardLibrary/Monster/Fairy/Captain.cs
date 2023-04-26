﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Card
{
    public Captain(CardInfo info) : base(info)
    {
        name = "妖精斥候";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Goblin;
        cost = 3;
        AddComponnet(new AttackComponent(4));
        AddComponnet(new AttackedComponent(4));
        var e=new SummonOnFirstRow(this,2,"绿妖精");
        AddComponnet(new SummonComponent(e));
        GetDesc = () => e.ToString();
    }
}