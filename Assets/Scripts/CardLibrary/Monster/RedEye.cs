using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEye : Card
{
    public RedEye()
    {
        name = "狂战士";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackComponent(2));
        //this.GetComponent<AttackComponent>().DebugShowAtk(this);
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new ActionComponent());
        AddComponnet(new UseComponent(new SummonComponent(this), 1));
        var m = new AfterSelfAttackListener(new SelfBuffEffect(2,2));
        AddComponnet(m);
        GetDesc = () => m.ToString();
    }
}
