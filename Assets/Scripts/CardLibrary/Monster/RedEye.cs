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
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new FieldComponnet());
        AddComponnet(new UseComponent(new SummonComponent(), 1));
        var m = new OnMoveBuffListener(2,2);
        AddComponnet(m);
        GetDesc = () => m.ToString();
    }
}
