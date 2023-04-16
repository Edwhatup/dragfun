using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerTotem : Card
{
    public AngerTotem(CardInfo info) : base(info)
    {
        name = "愤怒图腾";
        camp = CardCamp.Friendly;
        type = CardType.Construction;
        cost = 1;
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new SummonComponent());
        AddComponnet(new FieldComponnet(){canMove=0,canSwap=0,moveRange=0});
        var m = new AddAroundAtkHalo(this,3);
        AddComponnet(new HaloComponent(m));
        GetDesc = () => m.ToString();
    }
}