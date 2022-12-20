using System.Collections.Generic;
using System.Text;

public abstract class EventListenerComponent : CardComponent
{
    public abstract void EventListen(AbstractCardEvent e);
}
