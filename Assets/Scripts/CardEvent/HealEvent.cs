public class HealEvent : AbstractCardEvent
{
    public int healPoint;

    public HealEvent(Card source, Card target, int healPoint) : base(source, target)
    {
        this.healPoint = healPoint;
    }
}