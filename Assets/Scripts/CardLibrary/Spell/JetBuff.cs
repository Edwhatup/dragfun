using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBuff : Card
{
    public JetBuff(CardInfo info) : base(info)
    {
        name = "喷气背包";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 1;
        var e = new AddAtkRange(this,2);
        AddComponent(new SpellCastComponent(5,e));
        GetDesc=()=>e.ToString();    
    }
}
