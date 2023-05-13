using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMaster : Card
{
    public CreatureMaster(CardInfo info) : base(info)
    {
        name = "造型大师";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Haniwa;
        cost = 4;
        AddComponnet(new AttackComponent(4));
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new FieldComponnet());
        AddComponnet(new SummonComponent());

        var listener=new CreatureMasterListener(3,1);
        AddComponnet(listener);

        GetDesc = () => listener.ToString();

    }
}