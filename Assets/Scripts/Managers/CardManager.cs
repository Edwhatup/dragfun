using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour, IManager
{
    public static CardManager Instance { get; private set; }


    [SerializeField]
    RectTransform drawDeckTrans;
    [SerializeField]
    RectTransform discardDeckTrans;
    [SerializeField]
    RectTransform handTrans;
    [SerializeField]
    RectTransform enemyBoardTrans;



    public List<Card> cards = new List<Card>();
    public List<Card> enemies = new List<Card>();
    public List<Card> onBoardEnemies = new List<Card>();

    public List<Card> discardDeck = new List<Card>();
    public List<Card> drawDeck = new List<Card>();
    public List<Card> hand = new List<Card>();
    public List<Card> board = new List<Card>();
    public List<Card> consume = new List<Card>();
    public List<Card> used = new List<Card>();

    public List<Card> friendlyTombs = new List<Card>();
    public List<Card> enemyTombs = new List<Card>();


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

    public void AddNewCard2Board(Card wreckCard)
    {
        cards.Add(wreckCard);
        board.Add(wreckCard);
    }

    void OnDestory()
    {
        EventManager.Instance.eventListen -= BroadcastCardEvent;
    }
    #region IGameTurn实现区域

    public void GameStart()
    {
        ReadDeck();
        ReadEnemy();
        drawDeck.Shuffle();
        DrawCard(Player.Instance.initDrawCardCnt);
        Refresh();
    }

    public void UseCard(Card card)
    {
        if(card.type==CardType.Spell)
        {
            if (card.use.ConSume)
                hand.Transfer(consume, card);
            else hand.Transfer(discardDeck, card);
        }
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
            drawDeck.Transfer(hand, card);
            Refresh();
        }
        foreach(Card card in board)
        {
            card.Reset();
        }
    }
    public void Refresh()
    {

        foreach (Card card in hand)
            card.visual.transform.SetParent(handTrans.parent, false);
        foreach (Card card in hand)
            card.visual.transform.SetParent(handTrans, false);
        foreach (Card card in discardDeck)
            card.visual.transform.SetParent(discardDeckTrans, false);
        foreach (Card card in drawDeck)
            card.visual.transform.SetParent(drawDeckTrans, false);
        foreach (var card in cards)
            card.visual.UpdateVisual();
        LayoutRebuilder.ForceRebuildLayoutImmediate(handTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(drawDeckTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(discardDeckTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(enemyBoardTrans);
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
                var card = CardStore.Instance.CreateCard(item.Key);
                card.camp = CardCamp.Friendly;
                drawDeck.Add(card);
                cards.Add(card);
            }
        }
    }
    private void ReadEnemy()
    {
        string[] dataRow = DataManager.Instance.CurrentEnemyData.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray.Length < 2 || rowArray[0] == "name")
                continue;
            var enemy = CardStore.Instance.CreateCard(rowArray[0]);
            enemy.camp = CardCamp.Enemy;
            enemy.field.row = -1;
            enemy.field.col = null;
            enemies.Add(enemy);
            cards.Add(enemy);
            if(enemy.field.row>=0)
            {
                onBoardEnemies.Add(enemy);
            }
            enemy.visual.transform.SetParent(enemyBoardTrans,false);
        }
    }


    public void SummonCard(Card card)
    {
        hand.Transfer(board, card);
        if (!cards.Contains(card)) cards.Add(card);
        used.Add(card);
    }

    public void DestoryCardOnBoard(Card card)
    {
        card.field.cell.RemoveCard();
        if (card.type == CardType.Monster)
            board.Transfer(discardDeck, card);
        if (card.camp == CardCamp.Friendly)
            board.Transfer(friendlyTombs, card);
        else
            board.Transfer(enemyTombs, card);
    }



    public void BroadcastCardEvent(AbstractCardEvent cardEvent)
    {
        foreach (var card in drawDeck)
            BroadcastCardEvent2Card(card, cardEvent);
        foreach (var card in hand)
            BroadcastCardEvent2Card(card, cardEvent);
        foreach (var card in board)
            BroadcastCardEvent2Card(card, cardEvent);
        foreach (var card in enemies)
            BroadcastCardEvent2Card(card, cardEvent);
        foreach (var card in discardDeck)
            BroadcastCardEvent2Card(card, cardEvent);
    }
    private void BroadcastCardEvent2Card(Card card, AbstractCardEvent cardEvent)
    {
        foreach(var l in card.GetComponnets<EventListenerComponent>())
        {
            l.EventListen(cardEvent);
        }
    }

    public List<Card> GetSameRowEnemyUnits(Card card,Card target)
    {
        var sameRowTarget = cards
                            .FindAll(c => c.camp != card.camp && c.field.row != null && c.field.row == target.field.row)
                                .ToList();
        return sameRowTarget;
    }

    public List<Card> GetSameColEnemyUnits(Card card,Card target)
    {
        var sameColTarget = cards
                                  .FindAll(c => c.camp != card.camp && c.field.row != null && c.field.col == target.field.col)
                                   .ToList();
        return sameColTarget;
    }
    public List<Card> GetRoundFriendUnits(Card target)
    {
        var roundFriendTargets = cards
                                  .FindAll(c => c.camp == target.camp && (c.field.row == target.field.row+1||c.field.row == target.field.row-1||c.field.row == target.field.row) && (c.field.col == target.field.col+1||c.field.col == target.field.col-1||c.field.col == target.field.col)&&c!=target)
                                   .ToList();
        return roundFriendTargets;
    }
}
