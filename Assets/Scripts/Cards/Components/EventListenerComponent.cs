using System.Collections.Generic;
using System.Text;
public interface EventListenerAdapter
{
    string ToString();
    bool IsListenEvent(AbstractCardEvent e);
}

public abstract class EventListenerComponent : CardComponent
{
    protected CardEffect effect;
    public EventListenerComponent(CardEffect effect)
    {
        this.effect = effect;
    }
    public EventListenerComponent()
    {

    }
    public void Excute()
    {
        if (effect.CanUse())
            effect.Excute();
    }
    public abstract override string ToString();
    public abstract void EventListen(AbstractCardEvent e);
}

public class PassiveEffectComponent : EventListenerComponent
{
    public PassiveEffectComponent(PassiveCardEffect e) : base(e) { }

    public override void EventListen(AbstractCardEvent e)
    {
        ((PassiveCardEffect)effect).HandleEvent(e);
    }

    public override string ToString() => effect.ToString();
}