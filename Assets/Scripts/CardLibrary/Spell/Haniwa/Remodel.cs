using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remodel : Card
{
    public Remodel(CardInfo info) : base(info)
    {
        name = "重塑";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new ConditionalRandomSummon(this,CardRace.Haniwa,1);        
        AddComponnet(new SpellCastComponent(5, e));//效果还不对，没支持破坏
        GetDesc=()=>e.ToString();    
    }
}

