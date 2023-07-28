using System.Collections.Generic;

public abstract class CellEffect : CardEffect
{
    protected virtual List<Cell> Cells { get; } = new List<Cell>();

    public CellEffect(Card card) : base(card) { }

    // 判定是否在范围内
    public bool IsInRange(Cell targetCell)
    {
        return Cells.Contains(targetCell);
    }

    // 用他俩
    public abstract void Execute(Card c);
    public abstract void Undo(Card c);
}