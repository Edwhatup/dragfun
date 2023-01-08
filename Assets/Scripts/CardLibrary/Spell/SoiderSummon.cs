using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoiderSummon : Card
{
    public SoiderSummon()
    {
        name = "列阵";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        var e = new RandomSummonOnFirstRow(2,"士兵");
        AddComponnet(new UseComponent(new SpellCastComponent(this,3,e), 3));
        GetDesc=()=>e.ToString();    
    }
}

