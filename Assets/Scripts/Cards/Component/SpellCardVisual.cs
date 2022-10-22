using Card.Spell;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Card;
public class SpellCardVisual : CardVisual
{

    public Text nameText;
    public Text descText;
    public new SpellCard card=>base.card as SpellCard;
    
    
    public override void UpdateVisual()
    {
        if (nameText) nameText.text = card.name;
        if (descText) descText.text = card.GetDesc();
    }

}
