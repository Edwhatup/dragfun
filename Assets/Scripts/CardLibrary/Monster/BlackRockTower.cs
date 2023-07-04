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
        AddComponent(new AttackComponent(0));
        AddComponent(new AttackedComponent(3));
        var e= new BuffByBoardAtkCountListener(2,2,3);
        AddComponent(e);
        GetDesc = () => e.ToString();
    }
}
