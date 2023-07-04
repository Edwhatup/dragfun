using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEye : Card
{
    public RedEye(CardInfo info) : base(info)
    {
        name = "狂战士";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        cost = 1;
        AddComponent(new AttackComponent(2));
        AddComponent(new AttackedComponent(3));
        AddComponent(new SummonComponent());
        var m = new SelfAttackListener(new SelfBuffEffect(this,2, 2));
        AddComponent(m);
        GetDesc = () => m.ToString();
    }
}
