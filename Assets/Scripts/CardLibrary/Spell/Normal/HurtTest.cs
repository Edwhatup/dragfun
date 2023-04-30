using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtTest : Card
{
    public HurtTest(CardInfo info) : base(info)
    {
        name = "打自己人";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new DmgToFriend(this,3);
        AddComponnet(new SpellCastComponent(5, e));
        GetDesc=()=>e.ToString();    
    }
}
