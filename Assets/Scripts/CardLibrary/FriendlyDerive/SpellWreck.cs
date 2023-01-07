using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWreck : Card
{
    public SpellWreck(int age)
    {
        name = "法术残骸";
        camp = CardCamp.Friendly;
        type = CardType.FriendlyDerive;
        AddComponnet(new FieldComponnet()
        {
            canMove = 0,
            canSwap = 0,
        });
        AddComponnet(new WreckComponent(age));
        GetDesc = () => GetComponent<WreckComponent>().ToString();
    }

}
