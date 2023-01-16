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
        AddComponnet(new AttackedComponent(2));
        AddComponnet(new AttackComponent(3));
        var e = new MultiDamage2Enemy(this,14,5);
        AddComponnet(new SummonComponent(e));        
        GetDesc = ()=>GetComponent<SummonComponent>().ToString();
    }
}
