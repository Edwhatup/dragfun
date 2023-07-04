using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance => instance;
    private static ShopController instance;

    [SerializeField] private CardPack pack;
    [SerializeField] private Transform shopParent;
    [SerializeField] private Text moneyText;
    [SerializeField] private GameObject shopCard;
    [SerializeField] private string[] items;

    public ShopCard SelectedShopCard => selectedCard;
    private ShopCard selectedCard;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (var item in items)
        {
            var c = CardStore.Instance.CreateCard(item, false);
            var sC = Instantiate(shopCard, Vector3.zero, Quaternion.identity, shopParent).GetComponent<ShopCard>();
            sC.BindCard(c);
        }
        moneyText.text = Player.Instance.money.ToString();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void SelectShopCard(ShopCard item)
    {
        selectedCard = item;
        ShowDeck();
        pack.Entries.ForEach(i => i.Clicked += ReplaceCard);
    }

    public void ShowDeck()
    {
        pack.gameObject.SetActive(true);
        pack.UpdatePack();
    }

    public void HideDeck()
    {
        pack.Clear();
        pack.gameObject.SetActive(false);
        selectedCard = null;
    }

    private void ReplaceCard(CardEntry what)
    {
        var p = Player.Instance;
        p.deck[what.Card.name]--;
        if (p.deck[what.Card.name] == 0) p.deck.Remove(what.Card.name);

        if (p.deck.ContainsKey(selectedCard.Card.name)) p.deck[selectedCard.Card.name]++;
        else p.deck.Add(selectedCard.Card.name, 1);

        p.money -= selectedCard.Card.moneyCost;
        moneyText.text = Player.Instance.money.ToString();

        HideDeck();
    }
}
