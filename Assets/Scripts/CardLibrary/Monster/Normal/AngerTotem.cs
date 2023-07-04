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
        AddComponent(new AttackedComponent(3));
        AddComponent(new SummonComponent());
        AddComponent(new FieldComponnet(0, 0, 0));
        var m = new AddAroundAtkHalo(this, 3);
        AddComponent(new HaloComponent(m));
        GetDesc = () => m.ToString();
    }
}