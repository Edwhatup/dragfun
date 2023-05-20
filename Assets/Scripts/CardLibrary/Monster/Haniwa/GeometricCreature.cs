using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometricCreature : Card
{
    public GeometricCreature(CardInfo info) : base(info)
    {
        name = "几何造物";
        camp = CardCamp.Friendly;
        type = CardType.FriendlyDerive;
        race = CardRace.Haniwa;
        cost = 0;
        AddComponnet(new AttackComponent(1));
        AddComponnet(new AttackedComponent(1));
        AddComponnet(new ActionComponent());
        AddComponnet(new FieldComponnet());
        

        GetDesc = () => "";

    }
}
