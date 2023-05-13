using UnityEngine;
using UnityEngine.UI;
public class MonsterCardVisual : CardVisual, ISeletableTarget
{
    [SerializeField]
    Image selectableEdge;
    [HideInInspector]
    public Cell cell;
    [SerializeField] Text nameText;
    [SerializeField] Text descText;
    [SerializeField] Text atkText;
    [SerializeField] Text hpText;
    [SerializeField] Text costText;
    [SerializeField] Text buffText;
    public MonsterCardVisual(Card card)
    {
        this.card = card;
    }


    public override void UpdateVisual()
    {
        if (nameText) nameText.text = card.name;
        if (costText) costText.text = card.cost.ToString();
        if (hpText)
        {
            if (card.attacked != null)
            {
                hpText.gameObject.SetActive(true);
                hpText.text = card.attacked.ToString();
            }
            else
                hpText.gameObject.SetActive(false);
        }
        if (atkText)
        {
            if (card.attack != null)
            {
                atkText.gameObject.SetActive(true);
                atkText.text = card.attack.ToString();
            }
            else atkText.gameObject.SetActive(false);
        }
        if (descText) descText.text = card.GetDesc();
    }
    public void UpdateSelectableVisual(bool canSelect)
    {
        if (canSelect)
            selectableEdge.color = Color.green;
        else
            selectableEdge.color = Color.white;
    }

    public bool CanAsSelectSource()
    {
        return true;
    }
}
