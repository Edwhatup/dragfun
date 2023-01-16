using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiver : Card
{
    public Waiver(CardInfo info) : base(info)
    {
        name = "振荡器";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new SummonComponent());
        AddComponnet(new UseComponent(2));
        var e=new GroupDamage(this,1,RangeType.AllEnemiesOnBoard);
        var r=new ResonanceComponent(e);
        AddComponnet(r);
        GetDesc = () => e.ToString();
    }
}
