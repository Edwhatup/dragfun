public abstract class ConstantCellEffect : CellEffect
{
    protected abstract int LifeTime { get; }

    public int Timer { get; set; }
    private int timer = 0;

    protected ConstantCellEffect(Card card) : base(card)
    {
        timer = LifeTime;
    }

    public override sealed void Excute()
    {
        CardManager.Instance.ApplyCellEffect(this);
    }
}