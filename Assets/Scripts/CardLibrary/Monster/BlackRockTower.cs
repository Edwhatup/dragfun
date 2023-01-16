using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRockTower : Card
{
    public BlackRockTower(CardInfo info) : base(info)
    {
        name = "黑石塔";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Mech;
        cost = 1;
        AddComponnet(new AttackComponent(0));
        AddComponnet(new AttackedComponent(3));
        var e= new BuffByBoardAtkCountListener(2,2,3);
        AddComponnet(e);
        GetDesc = () => e.ToString();
    }
}
