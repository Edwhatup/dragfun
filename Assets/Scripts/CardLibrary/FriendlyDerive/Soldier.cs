using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Card
{
    public Soldier()
    {
        name = "士兵";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        AddComponnet(new AttackComponent(1){});
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
    }

}