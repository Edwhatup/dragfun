
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CanRepeat(false)]
public class WreckComponent : EventListenerComponent
{
    int pp;
    int timer;
    public WreckComponent(string[] args):base(null)
    {
        int.TryParse(args[0], out pp);
        this.timer = pp;
    }
    public WreckComponent(int pp):base(null)
    {
        this.pp = pp;
        this.timer = pp;
    }
    bool protect = false;
    public override void EventListen(AbstractCardEvent e)
    {    
        //忽略使用时的事件
        if(e is AfterUseEvent use)
        {
            if (use.source == card.source) return;
        }
        timer -= e.ppCost;
        if (timer <= 0 && !protect)
        {
            protect = true;
            card.field.state = BattleState.HalfDead;
            protect = false;
        }
    }
    public override string ToString()
    {
        return $"残留：{timer}";
    }
}
