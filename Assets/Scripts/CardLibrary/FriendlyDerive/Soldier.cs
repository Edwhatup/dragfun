using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Card
{
    public Soldier(int age)
    {
        name = "士兵";
        camp = CardCamp.Friendly;
        type = CardType.FriendlyDerive;
        AddComponnet(new AttackComponent(1){});
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        GetDesc = () => GetComponent<WreckComponent>().ToString();
    }

}