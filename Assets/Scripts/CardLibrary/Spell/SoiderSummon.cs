using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoiderSummon : Card
{
    public SoiderSummon(CardInfo info) : base(info)
    {
        name = "列阵";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 3;
        var e = new RandomSummonOnSpecificRow(this,2,"士兵",0);
        AddComponnet(new SpellCastComponent(3, e));
        GetDesc=()=>e.ToString();
    }
}

