using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoSingleton<CardManager>,IGameTurn
{
    [SerializeField]
    Transform deckTrans;
    [SerializeField]
    Transform handTrans;
    [SerializeField]
    GameObject cardPrefab;
    public List<Card> deck = new List<Card>();
    public List<Card> hand=new List<Card>();
    public List<Card> board=new List<Card>();  
    public Dictionary<Card,GameObject> deckCardObjects=new Dictionary<Card,GameObject>();
    public Dictionary<Card,GameObject> boardCardObjects=new Dictionary<Card,GameObject>();
    public Dictionary<Card, GameObject> handCardObjects=new Dictionary<Card, GameObject>();
#region IGameTurn实现区域
    public void EnemyAction()
    {
        
    }

    public void GameStart()
    {
        ReadDeck();
        ShuffletDeck();
        DrawCard(Player.Instance.initDrawCardCnt);   
    }

    public void PlayerAction()
    {

    }
    public void PlayDraw()
    {
        DrawCard(Player.Instance.drawCardCntTurn);
    }
    public void DrawCard(int cnt)
    {
        for(int i=0;i<cnt;i++)
        {
            if (deck.Count == 0) break;
            if (hand.Count == Player.Instance.maxHandCnt) break;
            Card card = deck[0];
            hand.Add(card);
            deck.RemoveAt(0);   
        }
    }
#endregion
    //根据玩家信息初始化卡组
    private void ReadDeck()
    {
        deck.Clear();
        foreach (var item in Player.Instance.deck)
        {
            for (int i = 0; i < item.Value; i++)
            {
                deck.Add(CardStore.Instance.CopyCard(item.Key));
            }
        }
    }
    //牌库洗牌
    private void ShuffletDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int rad = Random.Range(0, deck.Count);
            Card temp = deck[i];
            deck[i] = deck[rad];
            deck[rad] = temp;
        }
    }
    public void CreateOrUpdateCardObjects()
    {

    }
}
