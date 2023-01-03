using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefury:Card
{
    public Battlefury()
    {
        name = "狂战斧";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackedComponent(2));
        AddComponnet(new AttackComponent(3) { sweep=1});
        AddComponnet(new ActionComponent());
        AddComponnet(new UseComponent(new SummonComponent(this), 1));
        GetDesc = () => "横扫。";
    }
}
