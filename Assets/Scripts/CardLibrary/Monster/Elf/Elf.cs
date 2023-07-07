using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFairy : Card
{
    public GreenFairy(CardInfo info) : base(info)
    {
        name = "绿妖精";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Fairy;
        AddComponent(new AttackComponent(1){});
        AddComponent(new AttackedComponent(1));
        AddComponent(new ActionComponent());
        AddComponent(new SummonComponent());
        AddComponent(new UseComponent(1));
        GetDesc = () => "";
    }

}