using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetLeader : Card
{
    public JetLeader(CardInfo Info) : base(Info)
    {
        this.camp = CardCamp.Friendly;
        this.type = CardType.Monster;
        this.name = "河童飞行队长";
        cost = 4;
        var e = new RandomSummonOnSpecificRow(this,2,"动力河童",1);
        var listener = new ExtraRangeSummonListener(e,5);
        AddComponnet(new AttackedComponent(2));
        AddComponnet(new AttackComponent(3));
        AddComponnet(new SummonComponent());
        GetDesc = () => listener.ToString();
    }
}