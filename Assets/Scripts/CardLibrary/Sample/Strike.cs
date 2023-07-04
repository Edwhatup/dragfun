using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : Card
{
    public Strike(CardInfo info) : base(info)
    {
        name = "打击";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 1;
        var e = new SingleDamage2SpecifyEnemy(this,3);
        AddComponent(new SpellCastComponent(5, e));
        GetDesc=()=>e.ToString();    
    }
}
