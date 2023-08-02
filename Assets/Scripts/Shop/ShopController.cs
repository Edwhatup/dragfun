using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance => instance;
    private static ShopController instance;

    [SerializeField] private int rollCardCnt = 5;
    [SerializeField] private int maxRollTime = 1;
    [SerializeField] private int rollCost = 5;
    [SerializeField] private CardPack pack;
    [SerializeField] private Transform shopParent;
    [SerializeField] private Text moneyText;
    [SerializeField] private GameObject shopCard;
    [SerializeField] private GameObject rollBtn;
    [SerializeField] TextAsset shopConfig;

    public ShopCard SelectedShopCard => selectedCard;
    private ShopCard selectedCard;

    private int curIdx = -1;
    private int rollCnt = -1;
    private static Dictionary<string, int> shopPool = new Dictionary<string, int>();
    private static bool inited = false;

    [SerializeField] private string nextScene;

    private void Awake()
    {
        instance = this;
        if (!inited)
        {
            ReadShopItem(shopConfig);
            inited = true;
        }
    }

    private void Start()
    {
        RollCard();
        moneyText.text = Player.Instance.money.ToString();
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void RollCard()
    {
        if (rollCnt >= maxRollTime) { ShowRollTimeoutMessage(); return; }
        if (Player.Instance.money < rollCost) { ShowNoMoneyMessage(rollCost); return; }
        rollCnt++;
        Player.Instance.money -= rollCost;
        if (rollCnt == maxRollTime) rollBtn.SetActive(false);

        var items = new List<string>();
        for (int i = 0; i < rollCnt; i++)
        {
            var cnt = shopPool.Sum(j => j.Value);
            if (cnt == 0) break;
            var rand = Random.Range(0, cnt);
            foreach (var item in shopPool)
            {
                if (item.Value > rand)
                {
                    items.Add(item.Key);
                    shopPool[item.Key]--;
                    break;
                }
                rand -= item.Value;
            }
        }

        for (int i = 0; i < shopParent.childCount; i++) Destroy(shopParent.GetChild(i).gameObject);
        foreach (var item in items)
        {
            var c = CardStore.Instance.CreateCard(item, false);
            var sC = Instantiate(shopCard, Vector3.zero, Quaternion.identity, shopParent).GetComponent<ShopCard>();
            sC.BindCard(c);
        }
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
        selectedCard.gameObject.SetActive(false);

        HideDeck();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void ReadShopItem(TextAsset text)
    {
        var lines = text.text.Split('\n');
        foreach (var item in lines)
        {
            var result = item.Split(':');
            if (result.Length == 2)
            {
                if (int.TryParse(result[1], out var val))
                    if (shopPool.ContainsKey(result[0])) shopPool[result[0]] += val;
                    else shopPool.Add(result[0], val);
                else Debug.LogError($"商店配置: {result[0]} 需要指定一个数字作为数量");
            }
        }
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
