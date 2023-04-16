using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HaloEffect : CardEffect
{
    protected List<Cell> cells;
    protected List<Vector2Int> poses;
    protected bool isRelated;

    // 绝对坐标，例如第一排
    public HaloEffect(Card card, List<Cell> cells) : base(card)
    {
        this.cells = cells;
        isRelated = false;
    }

    // 相对坐标，例如可移动角色的四周
    public HaloEffect(Card card, List<Vector2Int> positions) : base(card)
    {
        poses = positions;
        isRelated = false;
    }

    // 判定是否在范围内
    public bool IsInRange(Cell targetCell)
    {
        if (!isRelated) return cells.Contains(targetCell);
        else
        {
            var self = card.field.cell;
            foreach (var pos in poses)
            {
                if (self.row + pos.x == targetCell.row && self.col + pos.y == targetCell.col) return true;
            }
            return false;
        }
    }

    // 不使用这个玩意了，封掉！
    public sealed override void Excute() { }

    // 用他俩
    public abstract void Execute(Card c);
    public abstract void Undo(Card c);
}
