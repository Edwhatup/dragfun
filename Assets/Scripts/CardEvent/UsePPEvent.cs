public class UsePPEvent : AbstractCardEvent
{
    public UsePPEvent(int pp) : base(null, null, pp) { }
}
public class AfterUseEvent : AbstractCardEvent
{
    public AfterUseEvent(Card source, int cost) : base(source, cost)
    {

    }
}