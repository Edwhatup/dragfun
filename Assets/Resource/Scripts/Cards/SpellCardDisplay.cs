using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCardDisplay :MonoBehaviour, IUpdateVisual, IPointerDownHandler,ISeletable
{
    public Text effectText;
    public Text nameText;
    public SpellCard card;

    public int TargetCount => card.targetConut;

    void Start()
    {
        //Selections.Instance.allCanSelections.Add(this);
        UpdateVisual();
    }
    
    public void UpdateVisual()
    {
        if(card!=null)
        {
            if (effectText) effectText.text = card.GetDesc();
            if (nameText) nameText.text = card.name;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanSelect())
        {
            //Selections.Instance.ClickSpellCard(this);
        }
    }

    public bool CanSelect()
    {
        if (Selections.Instance.HasSelectObject)
        {
            return false;
        }
        return true;
    }

    public void UpdateSelectableVisual()
    {

    }
}
