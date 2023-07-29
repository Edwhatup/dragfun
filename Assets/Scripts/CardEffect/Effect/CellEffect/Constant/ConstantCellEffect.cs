using UnityEngine;

public abstract class ConstantCellEffect : CellEffect
{
    public abstract int LifeTime { get; }

    protected int triggeredRow, triggeredCol;

    public int Timer { get; set; }
    private int timer = 0;

    protected ConstantCellEffect(Card card) : base(card)
    {
        timer = LifeTime;
    }

    public override sealed void Excute()
    {
        triggeredRow = (int)card.field.row;
        triggeredCol = (int)card.field.col;
        CardManager.Instance.ApplyCellEffect(this);
    }

    protected int StreetDistanceFromTriggered(Cell c)
        => StreetDistanceFromTriggered(c.row, c.col);

    protected int StreetDistanceFromTriggered(int row, int col)
        => Mathf.Abs(triggeredRow - row) + Mathf.Abs(triggeredCol - col);
}