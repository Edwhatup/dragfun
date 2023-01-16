using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recast : Card
{
    public Recast(CardInfo info) : base(info)
    {
        name = "重铸";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 1;
        var e = new RandomDamage2Enemy(this,3);
        //还没写完，应该改成破坏一个友方机械随从然后增加主动技能两点充能
        AddComponnet(new SpellCastComponent(5, e));
        GetDesc=()=>e.ToString();    
    }
}