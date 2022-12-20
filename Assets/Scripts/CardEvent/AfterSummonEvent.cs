//召唤事件，在召唤完成后广播
public class AfterSummonEvent : AbstractCardEvent
{
    public Cell summonCell;
    public AfterSummonEvent(Card card, Cell summonCell) : base(card, 0)
    {
        this.summonCell = summonCell;
    }
}