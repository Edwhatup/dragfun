using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthenResonance : Card
{
    public StrengthenResonance()
    {
        name = "强化共鸣";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        var e = new AddResonanceBuff();
        AddComponnet(new UseComponent(new SpellCastComponent(this,5,e), 1));
        GetDesc=()=>e.ToString();    
    }
}