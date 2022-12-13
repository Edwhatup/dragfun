
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


    List<AbstractCardEvent> eventCache=new List<AbstractCardEvent>();


    public void AddEvent2Cache(AbstractCardEvent cardEvent)
    {
        eventCache.Add(cardEvent);
    }
    public void ClearCache()
    {
        eventCache.Clear();
    }

    public void PassAllCacheEvent()
    {
        foreach(var e in eventCache)
            eventListen?.Invoke(e);
    }
    public void PassAllCacheEventAfter()
    {
        for (int i = eventCache.Count - 1; i >= 0; i--)
            eventListen?.Invoke(eventCache[i].EventAfter());
    }
    public void Clear()
    {
        eventCache.Clear();
    }
    public void PassEvent(AbstractCardEvent cardEvent)
    {
        eventListen?.Invoke(cardEvent);
    }


}