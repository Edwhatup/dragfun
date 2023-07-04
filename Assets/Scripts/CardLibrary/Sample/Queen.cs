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
        AddComponent(new AttackComponent(2));
        AddComponent(new AttackedComponent(3));
        var e = new RandomDamage2Enemy(this, 5);
        AddComponent(new ResonanceComponent(e));
        AddComponent(new SummonComponent(e));        
        GetDesc=()=>$"战吼,呼应:{e};";

    }
}
