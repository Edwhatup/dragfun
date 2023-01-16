using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEye : Card
{
    public RedEye(CardInfo info) : base(info)
    {
        name = "狂战士";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        cost = 1;
        AddComponnet(new AttackComponent(2));
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new SummonComponent());
        var m = new AfterSelfAttackListener(new SelfBuffEffect(this,2, 2));
        AddComponnet(m);
        GetDesc = () => m.ToString();
    }
}
