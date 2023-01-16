using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Card
{
    public Queen(CardInfo Info) : base(Info)
    {
        name = "皇后";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackComponent(2));
        AddComponnet(new AttackedComponent(3));
        var e = new RandomDamage2Enemy(this, 5);
        AddComponnet(new ResonanceComponent(e));
        AddComponnet(new SummonComponent(e));        
        GetDesc=()=>$"战吼,呼应:{e};";

    }
}
