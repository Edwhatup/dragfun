using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCreature : Card
{
    public SquareCreature(CardInfo info) : base(info)
    {
        name = "方形造物";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Haniwa;
        cost = 3;
        AddComponent(new AttackComponent(1));
        AddComponent(new AttackedComponent(4));
        AddComponent(new ActionComponent());
        AddComponent(new SummonComponent());
        AddComponent(new FieldComponnet());

        var e=new HaniwaOnBoardListener(1,1);
        AddComponent(e);
        
        var d=new DrawCard(this,1);
        AddComponent(new DeadComponent(d));

        GetDesc = () => e.ToString()+"死亡时："+d.ToString();
    }
}
