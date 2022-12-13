public abstract class AbstractCardEvent
{
    public enum CardEventType
    {
        Before,
        After,
    }
    public Card source = null;
    public Card target = null;
    public int ppCost = 0;
    public CardEventType type = CardEventType.Before;
    protected AbstractCardEvent(Card source, Card target, int ppCost)
    {
        this.source = source;
        this.target = target;
        this.ppCost = ppCost;
    }

    protected AbstractCardEvent(Card source, int ppCost)
    {
        this.source = source;
        this.ppCost = ppCost;
    }

    protected AbstractCardEvent(Card source)
    {
        this.source = source;
    }

    protected AbstractCardEvent(Card source, Card target) : this(source)
    {
        this.target = target;
    }
    public AbstractCardEvent EventAfter()
    {
        type = CardEventType.After;
        return this;
    }
}
