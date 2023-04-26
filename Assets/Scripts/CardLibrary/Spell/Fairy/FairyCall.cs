using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyCall : Card
{
    public FairyCall(CardInfo info) : base(info)
    {
        name = "妖精的呼朋引伴";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 3;
        var e = new RandomSummonOnSpecificRow(this,2,"绿妖精",0);
        AddComponnet(new SpellCastComponent(3, e));
        GetDesc=()=>e.ToString();
    }
}

