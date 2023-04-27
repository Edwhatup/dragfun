﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHaniwa : Card
{
    public SoldierHaniwa(CardInfo info) : base(info)
    {
        name = "战士埴轮";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Haniwa;
        cost = 3;
        AddComponnet(new AttackComponent(1));
        AddComponnet(new AttackedComponent(4));
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        AddComponnet(new FieldComponnet());

        var e=new HaniwaOnBoardListener(1,1);
        AddComponnet(e);
        
        var d=new DrawCard(this,1);
        AddComponnet(new DeadComponent(d));

        GetDesc = () => e.ToString();
    }
}