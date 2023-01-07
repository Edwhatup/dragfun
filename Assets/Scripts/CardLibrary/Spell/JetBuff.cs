using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBuff : Card
{
    public JetBuff()
    {
        name = "喷气背包";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        var e = new AddAtkRange(2);
        AddComponnet(new UseComponent(new SpellCastComponent(this,5,e), 1));
        GetDesc=()=>e.ToString();    
    }
}
