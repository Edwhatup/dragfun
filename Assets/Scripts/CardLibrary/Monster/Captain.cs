using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : Card
{
    public Captain()
    {
        name = "士官长";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackComponent(4));
        AddComponnet(new AttackedComponent(4));
        AddComponnet(new ActionComponent());
        var e=new SummonOnFirstRow(2,"士兵");
        AddComponnet(new UseComponent(new SummonComponent(this,e), 3));
        GetDesc = () => e.ToString();
    }
}
