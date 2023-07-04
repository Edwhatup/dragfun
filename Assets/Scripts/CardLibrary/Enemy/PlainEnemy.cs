using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainEnemy : Card
{
    public PlainEnemy(CardInfo info):base(info)
    {
        name = "地精";
        camp = CardCamp.Enemy;
        type = CardType.Enemy;
        AddComponent(new AttackedComponent(50) { bless=1,block=10});
        GetDesc = () => "只是个地精。";
    }

}
