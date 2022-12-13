using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum CardState
{
    InDrawDeck,
    InDiscardDeck,
    InHand,
    OnBoard,
    Consume,
    PreUse
}

public class CardManager : MonoBehaviour, IManager
{
    public static CardManager Instance { get; private set; }

    [SerializeField]
    RectTransform drawDeckTrans;
    [SerializeField]
    RectTransform discardDeckTrans;
    [SerializeField]
    RectTransform handTrans;

    public List<Card> allCards = new List<Card> { };


    public List<Card> discardDeck = new List<Card>();
    public List<Card> drawDeck = new List<Card>();
    public List<Card> hand = new List<Card>();
    public List<Card> board = new List<Card>();
    public List<Card> consume = new List<Card>();

    public List<string> tombs = new List<string>();
    public List<string> used = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            EventManager.Instance.eventListen += BroadcastCardEvent;
            DontDestroyOnLoad(gameObject);

        }
        else Destroy(gameObject);


    }
    void OnDestory()
    {
        EventManager.Instance.eventListen -= BroadcastCardEvent;
    }


    #region 获取卡牌信息的方法
    /// <summary>
    /// 获取满足条件的所有手牌
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Card> GetAllSpecifyCardsOnHand(Func<Card, bool> condition)
    {
        return hand.FindAll((e) => condition.Invoke(e));
    }
    /// <summary>
    /// 获取战场中满足条件的所有卡牌
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Card> GetAllSpecifyCardsOnBoard(Func<Card, bool> condition)
    {
        return board.FindAll((e) => condition.Invoke(e));
    }


    /// <summary>
    /// 随机获取满足条件的n张手牌,现有手牌数目不够n时返回所有满足条件的手牌
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Card> GetRandomSpecifyCardInHand(Func<Card, bool> condition, int n)
    {
        return hand.FindAll(e => condition.Invoke(e))
                      .GetRandomItems(n);
    }
    /// <summary>
    /// 随机获取战场中满足条件的n张卡牌,数目不够n时返回所有满足条件的卡牌
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Card> GetRandomSpecifyCardOnBoard(Func<Card, bool> condition, int n)
    {
        return board.FindAll(e => condition.Invoke(e))
                      .GetRandomItems(n);
    }
    public Card GetRandomSpecifyCardInHand(Func<Card, bool> condition)
    {
        return hand.FindAll(e => condition.Invoke(e))
                      .GetRandomItem();
    }
    public Card GetRandomSpecifyCardOnBoard(Func<Card, bool> condition)
    {
        return board.FindAll(e => condition.Invoke(e))
                      .GetRandomItem();
    }
    public Card GetMaxCardOnBoard(Func<Card, Card, int> comparer)
    {
        return board.GetMaxItem(comparer);
    }
    public Card GetMaxCardInHand(Func<Card, Card, int> comparer)
    {
        return hand.GetMaxItem(comparer);
    }
    public Card GetMinCardInHand(Func<Card, Card, int> comparer)
    {
        return hand.GetMinItem(comparer);
    }
    public Card GetMinCardOnBoard(Func<Card, Card, int> comparer)
    {
        return board.GetMinItem(comparer);
    }
    public List<Card> GetAllCardsInHand()
    {
        return hand.ToList();
    }
    public List<Card> GetAllCardsOnBoard()
    {
        return board.ToList();
    }
    public Card GetRandomCardInHand()
    {
        return hand.GetRandomItem();
    }
    public Card GetRandomCardOnBoard()
    {
        return board.GetRandomItem();
    }
    public List<Card> GetRandomCardsInHand(int n)
    {
        return hand.GetRandomItems(n);
    }
    public List<Card> GetRandomCardsOnBoard(int n)
    {
        return board.GetRandomItems(n);
    }
    #endregion
    #region IGameTurn实现区域

    public void GameStart()
    {
        ReadDeck();
        drawDeck.Shuffle();
        DrawCard(Player.Instance.initDrawCardCnt);
        Refresh();
    }
    public void DrawCard(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            if (drawDeck.Count == 0)
            {
                if (discardDeck.Count == 0) return;
                discardDeck.Shuffle();
                discardDeck.TransferAll(drawDeck);
            }
            if (hand.Count == Player.Instance.maxHandCnt) break;
            var card = drawDeck[0];
            hand.Add(card);
            card.state = CardState.InHand;
            drawDeck.Remove(card);
            Refresh();
        }
    }
    public void Refresh()
    {
        List<Card> removes = new List<Card>();
        foreach(Card card in hand)
        {
            if (card.state != CardState.PreUse)
                card.visual.transform.SetParent(handTrans.parent,false);
        }
        foreach (Card card in hand)
        {
            if (card.state != CardState.PreUse)
                card.visual.transform.SetParent(handTrans, false);
        }
        foreach (Card card in discardDeck)
            card.visual.transform.SetParent(discardDeckTrans, false);
        foreach (Card card in drawDeck)
            card.visual.transform.SetParent(drawDeckTrans, false);

        foreach (var card in allCards)
        {
            if (card.state == CardState.Consume)
                removes.Add(card);
            card.visual.UpdateVisual();
        }
        foreach (var r in removes)
        {
            allCards.Remove(r);
            Destroy(r.visual.gameObject);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(handTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(drawDeckTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(discardDeckTrans);
    }
    #endregion
    //根据玩家信息初始化卡组
    private void ReadDeck()
    {
        drawDeck.Clear();
        foreach (var item in Player.Instance.deck)
        {
            for (int i = 0; i < item.Value; i++)
            {
                var cardGo = CardStore.Instance.CreateCard(item.Key, null);
                var card = cardGo.GetComponent<CardVisual>().card;
                card.camp = CardCamp.Friendly;
                drawDeck.Add(card);
                allCards.Add(card);
            }
        }
    }

    public void UseSpell(Card card)
    {
        if (hand.Contains(card)) hand.Remove(card);
        if (!card.cast.Consume)
        {
            discardDeck.Add(card);
            card.state = CardState.InDiscardDeck;
        }
        else
        {
            consume.Add(card);
            card.state = CardState.Consume;
        }
    }
    public void SummonCard(Card card)
    {
        if (hand.Contains(card)) hand.Remove(card);
        board.Add(card);
        card.state = CardState.OnBoard;
        if(!allCards.Contains(card)) allCards.Add(card);
    }

    public void DestoryCardOnBoard(Card killer, Card card)
    {
        var deathEvent = new DeathEvent(killer, card);
        EventManager.Instance.PassEvent(deathEvent);
        switch (card.type)
        {
            case CardType.Monster:
                card.state = CardState.InDiscardDeck;
                break;
            case CardType.Derive:
                card.state = CardState.Consume;
                break;
        }
        card.field.state = BattleState.Dead;
        card.field.cell.RemoveCard();
        EventManager.Instance.PassEvent(deathEvent.EventAfter());
    }

    

    public void BroadcastCardEvent(AbstractCardEvent cardEvent)
    {
        Debug.Log(cardEvent.GetType().Name);
        foreach (var card in drawDeck)
        {
            card.listener?.EventPass(cardEvent);
        }
        foreach(var card in hand)
        {
            card.listener?.EventPass(cardEvent);
        }
        foreach(var card in board)
        {
            card.listener?.EventPass(cardEvent);
        }
        foreach (var card in discardDeck)
        {
            card.listener?.EventPass(cardEvent);
        }        
    }
}
