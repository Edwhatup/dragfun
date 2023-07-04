using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBuffer : Card
{
    public JetBuffer(CardInfo info) : base(info)
    {
        this.name = "强化动力工程师";
        this.camp = CardCamp.Friendly;
        this.type = CardType.Monster;
        cost = 1;
        AddComponent(new AttackedComponent(2));
        AddComponent(new AttackComponent(3));
        
        var e = new AddAtkRange(this,2);    //这个地方没写完，应该是加入卡组时，给一个具有增程的随从+3+3
        GetDesc=()=>e.ToString();    
    }
}
