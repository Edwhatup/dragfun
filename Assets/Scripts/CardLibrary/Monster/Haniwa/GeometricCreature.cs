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
        cost = 2;
        AddComponent(new AttackComponent(1));
        AddComponent(new AttackedComponent(1));
        AddComponent(new ActionComponent());
        AddComponent(new FieldComponnet());
        

        GetDesc = () => "";

    }
}
