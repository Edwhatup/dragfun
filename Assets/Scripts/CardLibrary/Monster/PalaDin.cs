using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalaDin:Card
{
    public PalaDin(CardInfo info) : base(info)
    {
        name = "圣骑士";
        camp = CardCamp.Friendly;
        type = CardType.Monster; 
        AddComponnet(new AttackComponent(1){atkRange=3});
        AddComponnet(new AttackedComponent(4) { bless=1,block=4});
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        AddComponnet(new UseComponent(3));
        GetDesc = () => "";
    }
}
