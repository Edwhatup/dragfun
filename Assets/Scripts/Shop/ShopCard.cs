using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopCard : CardEntry
{
    [SerializeField] private Text moneyText;

    public override void BindCard(Card c)
    {
        base.BindCard(c);
        moneyText.text = c.moneyCost.ToString();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (this == ShopController.Instance.SelectedShopCard) return;

        if (Card.moneyCost <= Player.Instance.money)
        {
            ShopController.Instance.SelectShopCard(this);
        }
        else
        {
            ShopController.Instance.ShowNoMoneyMessage(Card.moneyCost);
        }
    }
}
