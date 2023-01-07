using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBoy:Card
{
    public JetBoy()
    {
        name = "喷气地精";
        camp = CardCamp.Friendly;
        type = CardType.Monster; 
        AddComponnet(new AttackComponent(1){atkRange=2,buffAtkByRange=1});
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new ActionComponent());
        AddComponnet(new UseComponent(new SummonComponent(this), 1));
        GetDesc = () => "";
    }
}

