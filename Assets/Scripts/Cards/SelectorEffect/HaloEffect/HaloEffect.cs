using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HaloEffect : CardEffect
{
    protected virtual List<Cell> Cells { get; } = new List<Cell>();

    public HaloEffect(Card card) : base(card) { }

    // 判定是否在范围内
    public bool IsInRange(Cell targetCell)
    {
        return Cells.Contains(targetCell);
    }

    // 不使用这个玩意了，封掉！
    public sealed override void Excute() { }

    // 用他俩
    public abstract void Execute(Card c);
    public abstract void Undo(Card c);
}
