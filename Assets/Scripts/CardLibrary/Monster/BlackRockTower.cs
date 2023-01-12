using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackRockTower : Card
{
    public BlackRockTower()
    {
        name = "黑石塔";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Mech;
        AddComponnet(new AttackComponent(0));
        //this.GetComponent<AttackComponent>().DebugShowAtk(this);
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new ActionComponent());
        var e= new BuffByBoardAtkCountListener(2,2,3);
        AddComponnet(e);
        GetDesc = () => e.ToString();
    }
}
