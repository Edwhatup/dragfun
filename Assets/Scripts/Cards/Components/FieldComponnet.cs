using System;
using System.Collections.Generic;
using UnityEngine;

[CanRepeat(false)]
[RequireCardComponent(typeof(ActionComponent))]
public class FieldComponnet : CardComponent
{
    public Cell cell;
    public BattleState state;
    public int? row;
    public int? col;
    public int moveRange;
    public int canMove;
    public int canSwap;
    public bool CanMove => canMove > 0;
    public bool CanSwap => canSwap > 0;

    public FieldComponnet() : this(1, 1, 2)
    {
    }

    public FieldComponnet(int canMove = 1, int canSwap = 1, int moveRange = 2)
    {
        this.canMove = canMove;
        this.canSwap = canSwap;
        this.moveRange = moveRange;
        row = null;
        col = null;
    }
    public void Summon(Cell targetCell, bool isEffect = false)
    {
        AfterSummonEvent afe = new AfterSummonEvent(card, targetCell);
        targetCell.Summon(card);
        CardManager.Instance.SummonCard(card);
        GameManager.Instance.BroadcastCardEvent(afe);
    }

    public override void Recycle()
    {
        state = BattleState.Survive;
        row = null;
        col = null;
    }

    /// <summary>
    /// 是当前卡牌移动到指定单元格
    /// </summary>
    /// <param name="targetCell">目标场地</param>
    /// <param name="active">是否主动发起移动</param>
    public void Move(Cell targetCell, bool active, int cost = 0)
    {
        if (!(targetCell.CanMove() || targetCell.CanSwaped())) return;
        int ppcost = active ? cost : 0;
        if (!GameManager.Instance.TryCostPP(ppcost)) return;
        GameManager.Instance.BroadcastCardEvent(new BeforeMoveEvent(card, targetCell.card, cell, targetCell));
        if (targetCell.card != null)
        {
            var monster = targetCell.card;
            cell.Summon(monster);
        }
        // Debug.Log($"cell: {cell.row},{cell.col}\ntarget: {targetCell.row},{targetCell.col}");
        var e = new AfterMoveEvent(card, targetCell.card, cell, targetCell, ppcost);
        targetCell.Summon(card);
        GameManager.Instance.BroadcastCardEvent(e);
    }
}
