using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometricCreate : Card
{
    public GeometricCreate(CardInfo info) : base(info)
    {
        name = "几何造形";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new GeometricCreatureEffect(this,2,"几何造物");        
        AddComponent(new SpellCastComponent(5, e));
        GetDesc=()=>e.ToString();    
    }
}

