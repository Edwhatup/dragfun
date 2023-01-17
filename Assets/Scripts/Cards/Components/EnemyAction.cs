using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : CardComponent
{
    public EnemyEffectListener current;
    public void GetNextAction()
    {
        var ls=card.GetComponnets<EnemyEffectListener>();
        ls.Sort((l, r) => r.priority - l.priority);
        int maxP = GetMaxPriority(ls);
        var listeners=new List<EnemyEffectListener>();
        for(int i=0;i<ls.Count;i++)
        {
            if(ls[i].priority== maxP && ls[i].CanUse())
                listeners.Add(ls[i]);
        }
        current = listeners[Mathf.FloorToInt(GameManager.Instance.Random.value * listeners.Count)];
        current.Reset();
    }

    private int GetMaxPriority(List<EnemyEffectListener> ls)
    {
        return ls.GetMaxItem((l, r) => l.priority - r.priority).priority;
    }
}
