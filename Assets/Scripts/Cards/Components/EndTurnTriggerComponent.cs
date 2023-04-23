public class EndTurnTriggerComponent : EventListenerComponent
{
    public EndTurnTriggerComponent(CardEffect eff) : base(eff) { }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is EndTurnEvent)
        {
            effect.Excute();
        }
    }

    public override string ToString() => effect.ToString();
}
