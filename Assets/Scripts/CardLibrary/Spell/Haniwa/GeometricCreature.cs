using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometricCreature : Card
{
    public GeometricCreature(CardInfo info) : base(info)
    {
        name = "几何造物";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new ConditionalRandomSummon(this,CardRace.Haniwa,1);        
        AddComponnet(new SpellCastComponent(5, e));//效果还不对，没支持破坏，还在沿用重塑的效果
        GetDesc=()=>e.ToString();    
    }
}

