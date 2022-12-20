using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Card
{
    public Goblin()
    {
        name = "哥布林";
        AddComponnet(new AttackComponent(2));
        AddComponnet(new AttackedComponent(3));
        GetDesc = () => "";
    }
}
