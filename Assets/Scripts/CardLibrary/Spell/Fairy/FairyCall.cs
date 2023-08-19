using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyCall : Card
{
    public FairyCall(CardInfo info) : base(info)
    {
        name = "妖精的呼朋引伴";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        race = CardRace.Fairy;
        rarity = CardRarity.FirstBase;
        cost = 3;
        var e = new LifeEnergyCost(this,3,new DrawCard(this,2));
        AddComponent(new SpellCastComponent(3, e));
        GetDesc=()=>e.ToString();
    }
}

