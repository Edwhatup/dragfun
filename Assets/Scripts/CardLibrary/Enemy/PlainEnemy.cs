using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainEnemy : Card
{
    public PlainEnemy()
    {
        name = "地精";
        camp = CardCamp.Enemy;
        type = CardType.Enemy;
        AddComponnet(new AttackedComponent(50));
        GetDesc = () => "只是个地精。";
    }

}
