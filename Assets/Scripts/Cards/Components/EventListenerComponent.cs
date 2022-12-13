using System.Collections.Generic;
using System.Text;

public abstract class EventListenerComponent : CardComponent
{
    protected abstract void EventListen(AbstractCardEvent e);
    protected EventListenerComponent next=null;
    public void EventPass(AbstractCardEvent e)
    {
        EventListen(e);
        next?.EventListen(e);
    }

    public string GetDesc()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(Desc());
        sb.Append(";");
        if(next != null) sb.Append(next.Desc());
        sb.Remove(sb.Length - 1, 1);
        sb.Append("。");
        return sb.ToString();
    }
    public override void Add(CardComponent component)
    {
        if (component == this) return;
        if (component is EventListenerComponent eventListener)
        {
            eventListener.next = next;
            next = eventListener;
        }
    }
}
