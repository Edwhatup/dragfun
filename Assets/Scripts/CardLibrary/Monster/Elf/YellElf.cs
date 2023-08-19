using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendYellElf : Card
{
    public FriendYellElf(CardInfo info) : base(info)
    {
        name = "应援妖精";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Fairy;
        rarity = CardRarity.NoBase;
        AddComponent(new AttackComponent(1){});
        AddComponent(new AttackedComponent(1));
        AddComponent(new ActionComponent());
        AddComponent(new SummonComponent());
        AddComponent(new UseComponent(1));
        AddComponent(new PassiveEffectComponent(new SpreadAssist(this,1,1,0)));
        AddComponent(new DeadComponent(new ChargeLifeEnergy(this,1)));
        GetDesc = () => "";
    }
}
