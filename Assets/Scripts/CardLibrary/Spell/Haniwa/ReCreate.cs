using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReCreate : Card
{
    public ReCreate(CardInfo info) : base(info)
    {
        name = "重塑";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new ReCreateEffect(this,CardRace.Haniwa,1);        
        AddComponnet(new SpellCastComponent(5, e));
        GetDesc=()=>e.ToString();    
    }
}

