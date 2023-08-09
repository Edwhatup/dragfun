using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdolCreature : Card
{
    public IdolCreature(CardInfo info) : base(info)
    {
        name = "偶像造物";
        camp = CardCamp.Friendly;
        type = CardType.FriendlyDerive;
        race = CardRace.Haniwa;
        cost = 3;
        AddComponent(new AttackComponent(1));
        AddComponent(new AttackedComponent(1));
        AddComponent(new ActionComponent());
        AddComponent(new FieldComponnet());

        var listener = new IdolaListener();
        AddComponent(listener);

        GetDesc = () => "";

    }
}
