
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;    
        }
    }
    private EventManager() { }
    public event CardEventListen eventListen;

    public void PassEvent(AbstractCardEvent cardEvent)
    {
        eventListen?.Invoke(cardEvent);
    }


}