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
        cost = 2;
        AddComponent(new AttackComponent(4));
        AddComponent(new AttackedComponent(1));
        AddComponent(new ActionComponent());
        AddComponent(new FieldComponnet());
        AddComponent(new SummonComponent());

        var listener=new CreatureMasterListener(3,1);
        AddComponent(listener);

        GetDesc = () => listener.ToString();

    }
}