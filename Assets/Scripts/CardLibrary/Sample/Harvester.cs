using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : Card
{
    public Harvester(CardInfo Info) : base(Info)
    {
        this.camp = CardCamp.Friendly;
        this.type = CardType.Monster;
        this.name = "收割机";
        cost = 100;
        AddComponent(new AttackedComponent(2));
        AddComponent(new AttackComponent(3));
        var e = new MultiDamage2Enemy(this,14,5);
        AddComponent(new SummonComponent(e));        
        GetDesc = ()=>GetComponent<SummonComponent>().ToString();
    }
}
