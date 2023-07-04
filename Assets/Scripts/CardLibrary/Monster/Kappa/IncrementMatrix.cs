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
        AddComponent(new AttackedComponent(3));
        AddComponent(new SummonComponent());
        AddComponent(new FieldComponnet(){canMove=0,canSwap=0,moveRange=0});
        var m = new AddAroundAtkHalo(this,3);
        AddComponent(new HaloComponent(m));
        GetDesc = () => m.ToString();
    }
}