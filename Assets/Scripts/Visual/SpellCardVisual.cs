using UnityEngine;
using UnityEngine.UI;
public class SpellCardVisual : CardVisual, ISeletableTarget
{
    [SerializeField]
    Image selectableEdge; 
    [SerializeField] 
    Text nameText;
    [SerializeField]
    Text descText;


    public void UpdateSelectableVisual(bool canSelect)
    {
        if (canSelect)
            selectableEdge.color = Color.green;
        else
            selectableEdge.color = Color.white;
    }

    public override void UpdateVisual()
    {
        if (nameText) nameText.text = card.name;
        if (descText) descText.text = card.GetDesc();
    }

}
