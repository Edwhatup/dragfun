﻿using System;
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
    RectTransform enemyBoardTrans, enemyBelowBoardTrans;
    [SerializeField]
    RectTransform tombsTrans;


    public List<Card> cards = new List<Card>();
    public List<Card> Enemies => board.FindAll(c => c.camp == CardCamp.Enemy);

    public List<Card> EnemyDeriveOnBoard => Enemies.FindAll(c => c.field.row >= 0);

    public List<Card> FriendlyCardOnBoard => board.FindAll(c => c.camp == CardCamp.Friendly);

    public List<Card> FriendlyMonsterOnBorad => board.FindAll(c => c.type == CardType.Monster);

    public List<Card> FriendlyDeriveOnBorad => board.FindAll(c => c.type == CardType.FriendlyDerive);

    public List<Card> discardDeck = new List<Card>();
    public List<Card> drawDeck = new List<Card>();
    public List<Card> hand = new List<Card>();
    public List<Card> board = new List<Card>();
    public List<Card> consume = new List<Card>();
    public List<Card> used = new List<Card>();

    public List<Card> friendlyTombs = new List<Card>();
    public List<Card> enemyTombs = new List<Card>();

    public List<Card> requireRaceMonster = new List<Card>();

    public List<Card> enemies = new List<Card>();

    private List<ConstantCellEffect> cellEffects = new List<ConstantCellEffect>();


    void Awake()
    {
        // if (Instance == null)
        // {
        //     Instance = this;
        //     EventManager.Instance.eventListen += BroadcastCardEvent;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else Destroy(gameObject);
        Instance = this;
        EventManager.Instance.eventListen += BroadcastCardEvent;
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
        if (card.type == CardType.Spell)
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
                discardDeck.ForEach(p => { p.Recycle(); p.visual.UpdateVisual(); });
                discardDeck.TransferAll(drawDeck);
                Debug.Log($"抽卡堆更新, 剩余 {drawDeck.Count} ");

            }
            if (hand.Count == Player.Instance.maxHandCnt) break;
            var card = drawDeck[0];
            drawDeck.Transfer(hand, card);
            Debug.Log($"抽卡堆减少{cnt}张, 剩余 {drawDeck.Count} ");
            card.Init();
            Refresh();
        }
    }

    public void DrawSpecificRaceCard(int cnt, CardRace race)
    {
        var requireRaceMonster = drawDeck
                            .FindAll(c => c.type == CardType.Monster && c.race == race)
                                .GetRandomItems(cnt);
        foreach (Card card in requireRaceMonster)
        {
            drawDeck.Transfer(hand, card);
        }
        Debug.Log($"抽卡堆减少{cnt}张, 剩余 {drawDeck.Count} ");
    }

    public void DrawCardWithPPCost(int count = 1)
    {
        if (GameManager.Instance.TryCostPP(Player.Instance.drawPPCost * count))
        {
            DrawCard(count);
            EventManager.Instance.PassEvent(new UsePPEvent(Player.Instance.drawPPCost * count));
            GameManager.Instance.Refresh();
        }
    }

    public void Refresh()
    {
        DeadSettlement();
        foreach (Card card in hand)
            card.visual.transform.SetParent(handTrans.parent, false);
        foreach (Card card in hand)
            card.visual.transform.SetParent(handTrans, false);
        foreach (Card card in discardDeck)
            card.visual.transform.SetParent(discardDeckTrans, false);
        foreach (Card card in drawDeck)
            card.visual.transform.SetParent(drawDeckTrans, false);
        foreach (var card in enemyTombs)
            card.visual.transform.SetParent(tombsTrans, false);
        foreach (var card in friendlyTombs)
            card.visual.transform.SetParent(tombsTrans, false);
        foreach (var card in cards)
            card.visual.UpdateVisual();
        LayoutRebuilder.ForceRebuildLayoutImmediate(handTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(drawDeckTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(discardDeckTrans);
        LayoutRebuilder.ForceRebuildLayoutImmediate(enemyBoardTrans);
    }

    private void DeadSettlement()
    {
        bool flag = false;
        foreach (var card in board.ToList())
        {
            CheckCardState(card, ref flag);
        }
        if (Player.Instance.field.state != BattleState.Survive)
        {
            GameManager.Instance.GameFalse();
            return;
        }
        else if (Enemies.Count == 0)
        {
            GameManager.Instance.GamePass();
            return;
        }
        if (flag)
            DeadSettlement();
    }

    private void CheckCardState(Card card, ref bool flag)
    {
        if (card.field.state == BattleState.HalfDead)
        {
            card.field.state = BattleState.Dead;
            var deads = card.GetComponnets<DeadComponent>();
            foreach (var dead in deads)
            {
                dead.Excute();
            }
            BroadcastCardEvent(new DeathEvent(card.attacked.lastAttacker, card));
            if (enemies.Contains(card)) enemies.Remove(card);
            DestoryCardOnBoard(card);
            flag = true;
        }
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
                var info = new CardInfo()
                {
                    name = item.Key
                };
                var card = CardStore.Instance.CreateCard(info);
                card.camp = CardCamp.Friendly;
                drawDeck.Add(card);
                cards.Add(card);
            }
        }
    }
    private void ReadEnemy()
    {
        enemies.Clear();
        string[] dataRow = DataManager.Instance.CurrentEnemyData.Split('\n');
        float[] possibilities = dataRow.Select(item =>
        {
            if (float.TryParse(item.Split(':')[0], out var result)) return result;
            Debug.LogError($"错误: {item} 的概率不是小数!");
            return 0;
        }).ToArray();
        var rand = UnityEngine.Random.Range(0, possibilities.Sum());
        var i = 0;
        for (; i < possibilities.Length; i++)
        {
            rand -= possibilities[i];
            if (rand <= 0) break;
        }
        i = Mathf.Min(i, possibilities.Length - 1);
        var rowArr = dataRow[i].Split(':');
        if (rowArr.Length < 2)
        {
            Debug.LogError($"敌人配置文件的第 {i + 1} 行格式错误! \n需要有一个冒号分割概率与敌人配置");
            return;
        }
        var args = rowArr[1].TrimStart('[').TrimEnd(']').Split(',');

        bool side = true;
        for (int j = 0; j < args.Length; j += 2)
        {
            string name = args[j];
            int count = 0;
            if (int.TryParse(args[j + 1], out var result)) count = result;
            else Debug.LogError($"敌人配置文件的第 {i + 1} 行格式错误! \n需要按照: 敌人名称,数量,敌人名称,数量 的格式填写");

            for (int k = 0; k < count; k++)
            {
                var info = new CardInfo()
                { name = name };

                var enemy = CardStore.Instance.CreateCard(info);
                enemy.camp = CardCamp.Enemy;

                enemy.field.col = null;
                board.Add(enemy);
                cards.Add(enemy);
                if (side)
                {
                    enemy.field.row = -1;
                    enemy.visual.transform.SetParent(enemyBoardTrans, false);
                }
                else
                {
                    enemy.field.row = 5;
                    enemy.visual.transform.SetParent(enemyBelowBoardTrans, false);
                }
                side = !side;
                enemies.Add(enemy);
            }
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
        card.field.cell?.RemoveCard();

        switch (card.type)
        {
            case CardType.Enemy:
            case CardType.EnemyDerive:
                board.Transfer(enemyTombs, card);
                break;
            case CardType.Monster:
                board.Transfer(discardDeck, card);
                break;
            case CardType.FriendlyDerive:
                board.Transfer(friendlyTombs, card);
                break;
        }
    }

    public void BroadcastCardEvent(AbstractCardEvent cardEvent)
    {
        // Debug.Log(cardEvent.GetType());
        HandleEvent(cardEvent);
        foreach (var card in drawDeck.ToList())
            BroadcastCardEvent2Card(card, cardEvent);
        foreach (var card in hand.ToList())
            BroadcastCardEvent2Card(card, cardEvent);
        foreach (var card in board.ToList())
            BroadcastCardEvent2Card(card, cardEvent);
        foreach (var card in discardDeck.ToList())
            BroadcastCardEvent2Card(card, cardEvent);
    }

    private void BroadcastCardEvent2Card(Card card, AbstractCardEvent cardEvent)
    {
        // if (card.camp == CardCamp.Friendly)
        foreach (var l in card.GetComponnets<EventListenerComponent>())
        {
            l.EventListen(cardEvent);
        }
        // else
        // {
        //     card.enemyAction.current.EventListen(cardEvent);
        // }
    }

    private void HandleEvent(AbstractCardEvent e)
    {
        for (int i = cellEffects.Count - 1; i >= 0; i--)
        {
            var b = cellEffects[i];
            // Debug.Log("进行了场地光环检测");
            switch (e)
            {
                case AfterSummonEvent u:
                    // 是范围内的目标（非目标在开头除掉了），加光环
                    if (IsInCellEffectRange(b, u.source))
                    {
                        b.Execute(u.source);
                        // Debug.Log($"在光环范围内有目标被放置\n给 {u.source.name} 加了光环");
                    }
                    break;

                case AfterMoveEvent m:
                    var card = m.source;
                    // 别的卡进入，加光环
                    // Debug.Log($"目标{card.name}移动");
                    if (IsEnteringCellEffect(b, m))
                    {
                        // Debug.Log($"在光环范围内有目标被放置\n给 {m.source.name} 加了光环");
                        b.Execute(card);
                    }
                    // 别的卡退出，去光环
                    else if (IsExitingCellEffect(b, m)) b.Undo(card);
                    break;

                case DeathEvent d:
                    // 别的卡死亡, 去光环
                    if (IsCellEffectTarget(b, d.source)) b.Undo(d.source);
                    break;
            }

            if (b.Timer == -1) continue;
            else
            {
                b.Timer -= e.ppCost;
                if (b.Timer <= 0)
                {
                    GetAvailbleCellEffectTargets(b).ForEach(j => b.Undo(j));
                    cellEffects.Remove(b);
                }
            }
        }
    }

    public List<Card> GetSpecificAreaEnemies(Card card, Card target, RangeType range)
    {
        if (range == RangeType.SameRow)
        {
            var sameRowTarget = board
                            .FindAll(c => c.camp != card.camp && c.field.row != null && c.field.row == target.field.row)
                                .ToList();
            return sameRowTarget;
        }
        else if (range == RangeType.SameCol)
        {
            var sameColTarget = board
                                  .FindAll(c => c.camp != card.camp && c.field.row != null && c.field.col == target.field.col)
                                   .ToList();
            return sameColTarget;
        }
        else if (range == RangeType.Round)
        {
            var roundTargets = board
                                  .FindAll(c => c.camp != card.camp && (c.field.row == target.field.row + 1 || c.field.row == target.field.row - 1 || c.field.row == target.field.row) && (c.field.col == target.field.col + 1 || c.field.col == target.field.col - 1 || c.field.col == target.field.col) && c != target)
                                   .ToList();
            return roundTargets;
        }
        else if (range == RangeType.SmallCross)
        {
            var smallCrossTargets = board
                                  .FindAll(c => c.camp != card.camp && CellManager.Instance.GetStreetDistance(c.field.cell, card.field.cell) == 1)
                                    .ToList();
            return smallCrossTargets;
        }
        else
        {
            var allEnemies = board
                                  .FindAll(c => c.camp != card.camp)
                                   .ToList();
            return allEnemies;
        }
    }
    public List<Card> GetSpecificAreaFriends(Card card, Card target, RangeType range)
    {
        if (range == RangeType.SameRow)
        {
            var sameRowTarget = board
                            .FindAll(c => c.camp == card.camp && c.field.row != null && c.field.row == target.field.row)
                                .ToList();
            return sameRowTarget;
        }
        else if (range == RangeType.SameCol)
        {
            var sameColTarget = board
                                  .FindAll(c => c.camp == card.camp && c.field.row != null && c.field.col == target.field.col)
                                   .ToList();
            return sameColTarget;
        }
        else if (range == RangeType.Round)
        {
            var roundTargets = board
                                  .FindAll(c => c.camp == card.camp && (c.field.row == target.field.row + 1 || c.field.row == target.field.row - 1 || c.field.row == target.field.row) && (c.field.col == target.field.col + 1 || c.field.col == target.field.col - 1 || c.field.col == target.field.col) && c != target)
                                   .ToList();
            return roundTargets;
        }
        else if (range == RangeType.SmallCross)
        {
            var smallCrossTargets = board
                                  .FindAll(c => c.camp == card.camp && CellManager.Instance.GetStreetDistance(c.field.cell, card.field.cell) == 1)
                                    .ToList();
            return smallCrossTargets;
        }
        else
        {
            var allEnemies = board
                                  .FindAll(c => c.camp == card.camp)
                                   .ToList();
            return allEnemies;
        }

    }

    public List<Card> GetSpecificDeckMonster(CardRace race, int cnt)
    {
        var monsters = drawDeck
                                  .FindAll(c => c.race == race)
                                    .ToList()
                                    .GetRandomItems(cnt);
        return monsters;
    }
    public List<Card> GetSpecificDiscardMonster(CardRace race, int cnt)
    {
        var monsters = discardDeck
                                  .FindAll(c => c.race == race)
                                    .ToList()
                                    .GetRandomItems(cnt);
        return monsters;
    }

    public static string GetSpecificAreaName(RangeType range)
    {
        if (range == RangeType.SameRow)
        {
            return "同一行";
        }
        else if (range == RangeType.SameCol)
        {
            return "同一列";
        }
        else if (range == RangeType.Round)
        {
            return "周围8格";
        }
        else if (range == RangeType.SmallCross)
        {
            return "周围4格";
        }
        else
        {
            return "任意";
        }
    }


    public List<Card> GetRoundFriendUnits(Card target)
    {
        var roundFriendTargets = cards
                                  .FindAll(c => c.camp == target.camp && (c.field.row == target.field.row + 1 || c.field.row == target.field.row - 1 || c.field.row == target.field.row) && (c.field.col == target.field.col + 1 || c.field.col == target.field.col - 1 || c.field.col == target.field.col) && c != target)
                                   .ToList();
        return roundFriendTargets;
    }

    public List<Card> GetXRowMonsterUnits(int x)
    {
        var sameRowTarget = cards
                            .FindAll(c => c.type == CardType.Monster && c.field.row == x)
                                .ToList();
        return sameRowTarget;
    }

    public void ApplyCellEffect(ConstantCellEffect e)
    {
        if (!cellEffects.Contains(e))
        {
            cellEffects.Add(e);
            GetAvailbleCellEffectTargets(e).ForEach(c => e.Execute(c));
            Debug.Log("增加了场地效果!");
        }
    }

    public void RemoveCellEffect(ConstantCellEffect e)
    {
        if (cellEffects.Contains(e)) cellEffects.Remove(e);
    }

    public static bool OnBoard(Card card) => Instance.board.Contains(card);
    public static bool InDeck(Card card) => Instance.drawDeck.Contains(card);
    public static bool InDiscard(Card card) => Instance.discardDeck.Contains(card);
    public static bool InHand(Card card) => Instance.hand.Contains(card);

    // TODO: Implement this method
    public static bool InExile(Card card) => false;

    private List<Card> GetAvailbleCellEffectTargets(CellEffect b) => CardManager.Instance.board.FindAll(c => IsInCellEffectRange(b, c) && IsCellEffectTarget(b, c));

    private bool IsCellEffectTarget(CellEffect b, Card c) => CardTargetUtility.CardIsTarget(c, b.CardTargets);

    private bool IsInCellEffectRange(CellEffect b, Card c) => b.IsInRange(c.field.cell);

    private bool IsEnteringCellEffect(CellEffect b, AfterMoveEvent move) => b.IsInRange(move.targetCell) && !b.IsInRange(move.sourceCell);
    private bool IsExitingCellEffect(CellEffect b, AfterMoveEvent move) => b.IsInRange(move.sourceCell) && !b.IsInRange(move.targetCell);
}
