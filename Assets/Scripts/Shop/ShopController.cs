using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance => instance;
    private static ShopController instance;

    [SerializeField] private int rollCardCnt = 5;
    [SerializeField] private int maxRollTime = 1;
    [SerializeField] private int rollCost = 5, upgradeCost = 10;
    [SerializeField] private bool enableUpgrade = false;
    [SerializeField] private TextAsset shopRollRule;
    [SerializeField] private CardPack pack;
    [SerializeField] private Transform shopParent;
    [SerializeField] private Text moneyText;
    [SerializeField] private GameObject shopCard;
    [SerializeField] private GameObject rollBtn;
    [SerializeField] private GameObject upgradeBtn;
    // [SerializeField] TextAsset shopConfig;

    public ShopCard SelectedShopCard => selectedCard;
    private ShopCard selectedCard;

    private Predicate<Card> filter = null;

    private ShopPool shopPool => GameInstance.ShopPool;

    private int curIdx = -1;
    private int rolledCnt = -1;

    [SerializeField] private string nextScene;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        upgradeBtn.SetActive(enableUpgrade);

        RollCard(false);
        moneyText.text = Player.Instance.money.ToString();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void RollCard(bool spend = true)
    {
        if (rolledCnt >= maxRollTime) { ShowRollTimeoutMessage(); return; }
        if (spend && Player.Instance.money < rollCost) { ShowNoMoneyMessage(rollCost); return; }
        rolledCnt++;
        if (spend) Player.Instance.money -= rollCost;
        if (rolledCnt == maxRollTime) rollBtn.SetActive(false);

        var items = shopPool.FetchRandom(Player.Instance.ShopLvl, rollCardCnt);
        moneyText.text = Player.Instance.money.ToString();

        for (int i = 0; i < shopParent.childCount; i++) Destroy(shopParent.GetChild(i).gameObject);
        foreach (var item in items)
        {
            if (item == null) continue;
            var sC = Instantiate(shopCard, Vector3.zero, Quaternion.identity, shopParent).GetComponent<ShopCard>();
            sC.BindCard(item);
        }
    }

    public void SelectShopCard(ShopCard item)
    {
        selectedCard = item;
        if (Player.Instance.CardDeckFull)
        {
            ShowDeck();
            pack.Select(selectedCard.Card, new List<Func<Card, bool>>() { c => true },
                        l =>
                        {
                            var c = l[0];
                            selectedCard.gameObject.SetActive(false);
                            ReplaceCard(c);
                            pack.UpdatePack();
                            ExecuteOnBuyAction(selectedCard.Card);
                        });
        }
        else
        {
            Player.Instance.CardDeck.Add(item.Card);
            selectedCard.gameObject.SetActive(false);
            ExecuteOnBuyAction(item.Card);
        }
    }

    private void ExecuteOnBuyAction(Card c)
    {
        var s = c.GetComponent<ShopActionComponent>();
        if (s != null && s.BuyEffect != null)
        {
            ShowDeck();
            var b = s.BuyEffect;
            pack.Select(selectedCard.Card,
                    b.GetSelectTargets(),
                    d => { b.Execute(d); pack.UpdatePack(); },
                    b.AllowRepeat,
                    b.IncludeSelf);
        }
    }

    public void ShowDeck()
    {
        pack.gameObject.SetActive(true);
        pack.UpdatePack();
    }

    public void HideDeck()
    {
        if (pack.Selecting) return;
        pack.Clear();
        pack.gameObject.SetActive(false);
        selectedCard = null;
    }

    private void ReplaceCard(Card c)
    {
        var p = Player.Instance;
        var idx = p.CardDeck.IndexOf(c);
        p.CardDeck.Insert(idx, selectedCard.Card);
        p.CardDeck.Remove(c);

        p.money -= selectedCard.Card.moneyCost;
        moneyText.text = Player.Instance.money.ToString();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void Upgrade()
    {
        if (Player.Instance.money >= upgradeCost)
        {
            Player.Instance.ShopLvl++;
            Player.Instance.money -= upgradeCost;
        }
        else ShowNoMoneyMessage(upgradeCost);
    }

    /// <summary>
    /// 展示“刷新卡牌次数已用完”
    /// </summary>
    private void ShowRollTimeoutMessage()
    {

    }

    /// <summary>
    /// 展示“你钱不够”
    /// </summary>
    /// <param name="requiredMoney">需要的钱</param>
    public void ShowNoMoneyMessage(int requiredMoney)
    {

    }
}
