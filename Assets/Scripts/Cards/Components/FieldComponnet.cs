using System;
using System.Collections.Generic;
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
    public FieldComponnet()
    {
        this.canMove =1;
        this.canSwap = 1;
        this.moveRange = 1;
        row =null;
        col = null;
    }
    public void Summon(Cell targetCell, bool isEffect=false)
    {    
        AfterSummonEvent afe = new AfterSummonEvent(card, targetCell);
        targetCell.Summon(card);
        CardManager.Instance.SummonCard(card);
        GameManager.Instance.BroadcastCardEvent(afe);
    }

    /// <summary>
    /// 是当前卡牌移动到指定单元格
    /// </summary>
    /// <param name="targetCell">目标场地</param>
    /// <param name="active">是否主动发起移动</param>
    public void Move(Cell targetCell, bool active,int cost=0)
    {
        if (!(targetCell.CanMove() || targetCell.CanSwaped())) return;
        int ppcost = active ? cost : 0;
        if (targetCell.card != null)
        {
            var monster = targetCell.card;
            cell.Summon(monster);
        }
        targetCell.Summon(card);
        var e = new AfterMoveEvent(card, targetCell.card, cell, targetCell, ppcost);
        GameManager.Instance.BroadcastCardEvent(e);
    }
}
