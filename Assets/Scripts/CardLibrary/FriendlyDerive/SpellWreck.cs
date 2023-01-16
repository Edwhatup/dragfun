using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWreck : Card
{
    public SpellWreck(CardInfo info):base(info)
    {
        name = "法术残骸";
        camp = CardCamp.Friendly;
        type = CardType.FriendlyDerive;
        AddComponnet(new FieldComponnet()
        {
            canMove = 0,
            canSwap = 0,
        });
        AddComponnet(new WreckComponent(5));
        //AddComponnet(new WreckComponent(int.Parse(info.paras[0])));
        GetDesc = () => GetComponent<WreckComponent>().ToString();
    }

}
