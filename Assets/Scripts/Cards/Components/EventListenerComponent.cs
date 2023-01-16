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
        this.effect =  effect ;
    }
    public void Excute()
    {
        if (effect.CanUse())
            effect.Excute();
    }
    public abstract override string ToString();
    public abstract void EventListen(AbstractCardEvent e);
}
