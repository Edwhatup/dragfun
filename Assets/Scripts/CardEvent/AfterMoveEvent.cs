﻿//随从移动事件
public class AfterMoveEvent : AbstractCardEvent
{
    //初始cell
    public Cell sourceCell;
    //目标cell
    public Cell targetCell;

    public bool IsSwap()
    {
        return target != null;
    }
    //sourceMonster的移动是否为前进
    public bool IsSourceForward()
    {
        return CellManager.Instance.GetRowDistance(targetCell, sourceCell) < 0;
    }
    //targetMonster的移动是否为前进
    public bool IsTargetForward()
    {
        return CellManager.Instance.GetRowDistance(sourceCell, targetCell) < 0;
    }
    //发生移动的monster个数
    public int MoveMonsterCount()
    {
        return 1 + (target == null ? 0 : 1);
    }
    public AfterMoveEvent(Card monster, Cell oldCell, Cell newCell, int ppCost) : base(monster, ppCost)
    {
        this.sourceCell = oldCell;
        this.targetCell = newCell;
    }



    public AfterMoveEvent(Card sourceMonster, Card targetMonster, Cell oldCell, Cell newCell, int ppCost) : base(sourceMonster, targetMonster, ppCost)
    {
        this.sourceCell = oldCell;
        this.targetCell = newCell;
    }
}