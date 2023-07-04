using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontlineSupport : Card
{
    public FrontlineSupport(CardInfo info) : base(info)
    {
        name = "前线支援";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        var e = new AddFirstRowArmour(this,2);
        AddComponent(new SpellCastComponent(5,e));
        GetDesc=()=>e.ToString();
    }
}

