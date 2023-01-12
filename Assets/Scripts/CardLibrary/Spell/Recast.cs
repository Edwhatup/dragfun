using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recast : Card
{
    public Recast()
    {
        name = "重铸";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        var e = new RandomDamage2Enemy(3);
        //还没写完，应该改成破坏一个友方机械随从然后增加主动技能两点充能
        AddComponnet(new UseComponent(new SpellCastComponent(this,5, e), 1));
        GetDesc=()=>e.ToString();    
    }
}