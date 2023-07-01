using UnityEngine;

public class DirectAtkCountdownComponent : EventListenerComponent
{
    public bool Ready => timer == 0;

    private int cd;
    private int timer;

    public DirectAtkCountdownComponent(int cd)
    {
        this.cd = cd;
        timer = cd;
    }

    public override void EventListen(AbstractCardEvent e)
    {
        Debug.Log("gg");
        timer -= e.ppCost;
        if (timer < 0) timer = 0;
        card.visual.UpdateVisual();
    }

    public override string ToString()
    {
        return "" + timer;
    }
}