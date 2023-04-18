using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : CardComponent
{
    public EnemyEffectListener current;
    public void GetNextAction()
    {
        // 获取所有Listener
        var ls = card.GetComponnets<EnemyEffectListener>();
        if (ls.Count == 0) return;

        // 删除当前的 (防止重复)
        if (ls.Contains(current)) ls.Remove(current);

        // 删除不符合条件的
        for (int i = ls.Count - 1; i >= 0; i--) if (!ls[i].Check()) ls.RemoveAt(i);

        if (ls.Count == 0)
        {
            current = card.GetComponnets<EnemyEffectListener>().Find(l => l.priority == 0);
        }
        else
        {
            ls.Sort((l, r) => r.priority - l.priority);
            int maxP = GetMaxPriority(ls);
            var list = ls.FindAll(l => l.priority == maxP);
            current = list[Mathf.FloorToInt(GameManager.Instance.Random.value * list.Count)];
        }
        current.Reset();
    }

    private int GetMaxPriority(List<EnemyEffectListener> ls)
    {
        return ls.GetMaxItem((l, r) => l.priority - r.priority).priority;
    }
}
