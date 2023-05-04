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
        cost = 6;
        AddComponnet(new AttackComponent(3));
        AddComponnet(new AttackedComponent(6));
        AddComponnet(new ActionComponent());
        AddComponnet(new FieldComponnet());

        var e = new TauntBuff();
        this.AddBuff(e);

        var d = new GroupStatsBuff(this,2,2,CardRace.Haniwa);
        AddComponnet(new DeadComponent(d));

        GetDesc = () => e.ToString()+",死亡时："+d.ToString();

    }
}
