public class EndTurnTriggerComponent : EventListenerComponent
{
    public override void EventListen(AbstractCardEvent e)
    {
        if (e is EndTurnEvent)
        {
            effect.Excute();
        }
    }

    public override string ToString() => effect.ToString();
}
