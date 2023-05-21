using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MonsterCardVisual : CardVisual, ISeletableTarget, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Image selectableEdge;
    [HideInInspector]
    public Cell cell;
    [SerializeField] Text nameText;
    [SerializeField] Text descText;
    [SerializeField] GameObject atkText;
    [SerializeField] GameObject hpText;
    [SerializeField] GameObject costText;
    [SerializeField] GameObject buffText;



    public MonsterCardVisual(Card card)
    {
        this.card = card;
    }


    public override void UpdateVisual()
    {
        if (nameText) nameText.text = card.name;
        if (costText) 
            {
                GameObject CostText = costText.transform.Find("CostText").gameObject;
                CostText.GetComponent<Text>().text = card.cost.ToString();
            }
        if (hpText)
        {
            if (card.attacked != null)
            {
                hpText.gameObject.SetActive(true);
                GameObject HpText = hpText.transform.Find("HealthText").gameObject;
                HpText.GetComponent<Text>().text = card.attacked.ToString();

            }
            else
                hpText.gameObject.SetActive(false);
        }
        if (atkText)
        {
            if (card.attack != null)
            {
                atkText.gameObject.SetActive(true);
                Text AtkText = atkText.transform.Find("AttackText").gameObject.GetComponent<Text>();
                AtkText.text = card.attack.ToString();
            }
            else 
                atkText.gameObject.SetActive(false);

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        buffText.gameObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buffText.gameObject.SetActive(false);
    }

    
}
