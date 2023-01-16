using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrsDead : Card
{
    public MrsDead(CardInfo Info) : base(Info)
    {
        this.camp = CardCamp.Friendly;
        this.type = CardType.Monster;
        this.name = "王女士";
        cost = 1;
        var e = new RandomDamage2Enemy(this, 6);
        var dead = new DeadComponent(e);
        AddComponnet(dead);
        AddComponnet(new AttackedComponent(2));
        AddComponnet(new AttackComponent(3));
        AddComponnet(new SummonComponent());
        GetDesc = () => dead.ToString();
    }
}
