public class AfterSelfMoveListener : EventListenerComponent
{
    public AfterSelfMoveListener(CardEffect effect) : base(effect)
    { }
    public override string ToString()
    {
        return $"移动时," + effect.ToString();
    }
    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterMoveEvent && e.source == card)
            Excute();
    }
}
public class AfterMoveListener : EventListenerComponent
{
    public AfterMoveListener(CardEffect effect) : base(effect) { }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterMoveEvent)
            Excute();
    }

    public override string ToString()
    {
        return $"在一名随从移动后," + effect.ToString();
    }
}