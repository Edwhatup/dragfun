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
        AddComponnet(new AttackComponent(1){});
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        AddComponnet(new UseComponent(1));
        GetDesc = () => "";
    }

}