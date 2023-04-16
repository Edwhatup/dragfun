using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementMatrix : Card
{
    public IncrementMatrix(CardInfo info) : base(info)
    {
        name = "增幅矩阵";
        camp = CardCamp.Friendly;
        type = CardType.Construction;
        cost = 1;
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new SummonComponent());
        AddComponnet(new FieldComponnet(){canMove=0,canSwap=0,moveRange=0});
        var m = new AddAtkHalo(card,3);
        AddComponnet(new HaloComponent(m));
        GetDesc = () => m.ToString();
    }
}