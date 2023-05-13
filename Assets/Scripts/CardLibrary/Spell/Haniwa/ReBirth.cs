using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReBirth : Card
{
    public ReBirth(CardInfo info) : base(info)
    {
        name = "造型新生";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new RebirthEffect(this,1);        
        AddComponnet(new SpellCastComponent(4, e));
        GetDesc=()=>e.ToString();    
    }
}

