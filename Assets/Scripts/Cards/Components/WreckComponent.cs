
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckComponent : EventListenerComponent
{
    int pp;
    int timer;
    public WreckComponent(string[] args)
    {
        int.TryParse(args[0], out pp);
        this.timer = pp;
    }
    public WreckComponent(int pp)
    {
        this.pp = pp;
        this.timer = pp;
    }
    bool protect = false;
    protected override void EventListen(AbstractCardEvent e)
    {    
        //忽略使用时的事件
        if(e is UseEvent use)
        {
            if (use.source == card.source) return;
        }
        if (e.type == AbstractCardEvent.CardEventType.After) 
        {
            Debug.Log(e.ppCost);
            timer -= e.ppCost;
            if (timer == 0 && !protect)
            {
                protect = true;
                CardManager.Instance.DestoryCardOnBoard(card,card);
                protect = false;
            }
        }
    }

    public override string Desc()
    {
        return $"残留：{timer}";
    }
}
