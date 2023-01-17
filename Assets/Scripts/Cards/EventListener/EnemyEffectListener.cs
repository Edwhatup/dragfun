using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireCardComponent(typeof(EnemyAction))]
[CanRepeat(true)]
public class EnemyEffectListener : EventListenerComponent
{
    public enum Type
    {
        Loop,
        Once
    }
    public int pp;
    public int timer;
    public int priority = 0;
    bool canUse = false;
    public Type type = Type.Loop;
    public bool CanUse()
    {
        return effect.CanUse();
    }
    public EnemyEffectListener(int pp, CardEffect effect) : base(effect)
    {
        this.pp = pp;
    }

    public void Reset()
    {
        timer = pp;
        canUse = true;
    }


    public override string ToString()
    {
        return $"倒计时：{timer}  {effect.ToString()}。";
    }
    bool protect=false;
    public override void EventListen(AbstractCardEvent e)
    {
        if (!canUse) return;
        this.timer -= e.ppCost;
        Debug.Log(e.GetType().Name + e.ppCost);
        if (this.timer <= 0 && !protect)
        {
            protect = true;
            Excute();
            switch (type)
            {
                case Type.Loop:
                    break;
                case Type.Once:
                    card.RemoveComponnent(this);
                    break;
            }
            canUse = false;
            card.enemyAction.GetNextAction();
            protect = false;
        }

    }
}
