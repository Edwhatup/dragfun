using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlefury:Card
{
    public Battlefury(CardInfo info) : base(info)
    {
        name = "狂战斧";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        cost = 1;
        AddComponnet(new AttackedComponent(2));
        AddComponnet(new AttackComponent(3) { sweep=1});
        AddComponnet(new SummonComponent());
        GetDesc = () => "横扫。";
    }
}
