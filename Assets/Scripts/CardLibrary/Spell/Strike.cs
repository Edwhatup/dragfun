using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Card
{
    public Strike()
    {
        name = "打击";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        var e = new RandomDamage2Enemy(3);
        AddComponnet(new UseComponent(new SpellCastComponent(this,5, e), 1));
        GetDesc=()=>e.ToString();    
    }
}
