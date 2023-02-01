﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 放第一排的时候永久加攻，放后面转化输出
/// </summary>
public class OldAss:Card
{
    public OldAss(CardInfo info) : base(info)
    {
        name = "老佣兵";
        camp = CardCamp.Friendly;
        type = CardType.Monster; 
        AddComponnet(new AttackComponent(1));
        AddComponnet(new AttackedComponent(4));
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        AddComponnet(new UseComponent(3));
        //没写完
        GetDesc = () => "";
    }
}