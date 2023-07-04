using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianHaniwa : Card
{
    public GuardianHaniwa(CardInfo info) : base(info)
    {
        name = "卫士埴轮";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Haniwa;
        cost = 3;
        AddComponent(new AttackComponent(3));
        AddComponent(new AttackedComponent(6));
        AddComponent(new ActionComponent());
        AddComponent(new FieldComponnet());
        

        var e = new GetTaunt(this);
        AddComponent(new SummonComponent(e));

        var d = new GroupStatsBuff(this,2,2,CardRace.Haniwa);
        AddComponent(new DeadComponent(d));

        GetDesc = () => e.ToString()+",死亡时："+d.ToString();

    }
}
