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
        AddComponent(new AttackComponent(1){atkRange=3});
        AddComponent(new AttackedComponent(4) { bless=1,block=4});
        AddComponent(new ActionComponent());
        AddComponent(new SummonComponent());
        AddComponent(new UseComponent(3));
        GetDesc = () => "";
    }
}
