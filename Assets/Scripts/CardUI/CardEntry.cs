using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardEntry : MonoBehaviour, IPointerClickHandler
{
    public delegate void CardEntryClickedEvent(CardEntry what);
    public event CardEntryClickedEvent Clicked;

    [SerializeField] private Image icon;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descText;
    [SerializeField] private GameObject atkText;
    [SerializeField] private GameObject hpText;
    [SerializeField] private Text costText;
    // [SerializeField] private GameObject buffText;

    public Card Card => card;
    private Card card;

    public virtual void BindCard(Card c)
    {
        // icon.overrideSprite = c.
        card = c;

        nameText.text = c.name;
        descText.text = c.GetDesc();

        if (c.attack != null)
            atkText.GetComponentInChildren<Text>().text = c.attack.atk.ToString();
        else atkText.SetActive(false);

        if (c.attacked != null)
            hpText.GetComponentInChildren<Text>().text = c.attacked.maxHp.ToString();
        else hpText.SetActive(false);

        costText.text = c.cost.ToString();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(this);
    }
}
