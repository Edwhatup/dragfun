//召唤事件，在召唤完成后广播
public class SummonEvent : AbstractCardEvent
{
    public Cell summonCell;
    public SummonEvent(Card card, Cell summonCell) : base(card, 0)
    {
        this.summonCell = summonCell;
    }
}