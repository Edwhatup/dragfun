using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthenResonance : Card
{
    public StrengthenResonance(CardInfo info) : base(info)
    {
        name = "强化共鸣";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 5;
        var e = new AddResonanceBuff(this);
        AddComponent(new SpellCastComponent(5,e));
        GetDesc=()=>e.ToString();    
    }
}