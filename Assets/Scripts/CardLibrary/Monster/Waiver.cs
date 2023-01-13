using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiver : Card
{
    public Waiver()
    {
        name = "振荡制造机";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new UseComponent(new SummonComponent(this), 2));
        var e=new GroupDamage(1,RangeType.AllEnemies);
        var r=new ResonanceComponent(e);
        AddComponnet(r);
        GetDesc = () => r.ToString();
    }
}
