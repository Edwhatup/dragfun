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
        cost = 0;
        AddComponnet(new AttackComponent(1));
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new FieldComponnet());
        //没做完

        GetDesc = () => "";

    }
}
