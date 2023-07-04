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
        AddComponent(dead);
        AddComponent(new AttackedComponent(2));
        AddComponent(new AttackComponent(3));
        AddComponent(new SummonComponent());
        GetDesc = () => dead.ToString();
    }
}
