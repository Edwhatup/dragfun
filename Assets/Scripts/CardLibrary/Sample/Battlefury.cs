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
        AddComponent(new AttackedComponent(2));
        AddComponent(new AttackComponent(3) { sweep=1});
        AddComponent(new SummonComponent());
        GetDesc = () => "横扫。";
    }
}
