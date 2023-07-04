using System.Collections;
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
        AddComponent(new AttackComponent(1));
        AddComponent(new AttackedComponent(4));
        AddComponent(new ActionComponent());
        AddComponent(new SummonComponent());
        AddComponent(new UseComponent(3));
        //没写完
        GetDesc = () => "";
    }
}