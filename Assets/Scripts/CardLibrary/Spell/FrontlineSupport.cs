using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontlineSupport : Card
{
    public FrontlineSupport()
    {
        name = "前线支援";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        var e = new AddFirstRowArmour(2);
        AddComponnet(new UseComponent(new SpellCastComponent(this,5,e), 1));
        GetDesc=()=>e.ToString();    
    }
}

