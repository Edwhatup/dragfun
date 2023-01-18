using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBoy:Card
{
    public JetBoy(CardInfo info) : base(info)
    {
        name = "喷气地精";
        camp = CardCamp.Friendly;
        type = CardType.Monster; 
        cost = 2;
        AddComponnet(new AttackComponent(1){atkRange=2,buffAtkByRange=1});
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        AddComponnet(new UseComponent(2));
        GetDesc = () => "增程1，攻击造成额外等于射程的伤害";
    }
}

