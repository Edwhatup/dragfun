using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Goblin : Card
{
    public Goblin()
    {
        

        name = "哥布林";
        AddComponnet(new AttackComponent(2));
        AddComponnet(new AttackedComponent(3));
        AddComponnet(new ActionComponent());
        GetDesc = () => "";
    }
}
