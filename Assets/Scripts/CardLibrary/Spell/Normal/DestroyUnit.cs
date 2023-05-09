using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUnit : Card
{
    public DestroyUnit(CardInfo info) : base(info)
    {
        name = "星弧破碎";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new DestroyUnitEffect(this);
        AddComponnet(new SpellCastComponent(5, e));
        GetDesc=()=>e.ToString();    
    }
}
