using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiver : Card
{
    public Waiver(CardInfo info) : base(info)
    {
        name = "振荡制造机";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        cost=2;
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        var e=new GroupDamage(this,1,RangeType.AllEnemiesOnBoard);
        var r=new ResonanceComponent(e);
        AddComponnet(r);
        GetDesc = () => r.ToString();
    }
}
