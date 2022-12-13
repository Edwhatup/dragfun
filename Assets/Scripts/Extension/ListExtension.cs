
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtension
{
    public static T Back<T>(this List<T> list)
    {
        if (list.Count == 0) return default(T);
        return list[list.Count - 1];
    }
    /// <summary>
    /// 打乱集合
    /// </summary>
    /// <param name="timesBase">打断次数为集合长度*timesBase</param>
    /// <returns></returns>
    public static void Shuffle<T>(this List<T> list, int timesBase=2)
    {
        int cnt=list.Count;
        int t=timesBase* cnt;
        for(int i=0; i<t; i++)
        {
            int rad1 = UnityEngine.Random.Range(0, cnt);
            int rad2 = UnityEngine.Random.Range(0, cnt);
            var temp = list[rad1];
            list[rad1] = list[rad2];
            list[rad2] = temp;
        }
    }
    public static void TransferAll<T>(this List<T> list,List<T> target)
    {
        int cnt = list.Count;
        for (int i = 0; i < cnt; i++)
        {
            target.Add(list[0]);
            list.RemoveAt(0);
        }
    }
    public static void Transfer<T>(this List<T> list,List<T> target,T item)
    {
        if(list.Contains(item)) list.Remove(item);  
        if(!target.Contains(item)) target.Add(item);
    }
    public static List<T> GetRandomItems<T>(this List<T> list,int n)
    { 
        var res=new List<T>();
        var listCopy = list.ToList();
        for (int i = 0; i < n && listCopy.Count > 0; i++)
        {
            var enemy = listCopy[UnityEngine.Random.Range(0, listCopy.Count)];
            res.Add(enemy);
            listCopy.Remove(enemy);
        }
        return res;
    }
    public static T GetRandomItem<T>(this List<T> list)
    {
        if(list.Count == 0) return default(T);
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    public static bool Has<T>(this List<T> list, Func<T, bool> condition)
    {
        return list.Find((e)=>condition(e))!=null;
    }
    public static T GetMinItem<T>(this List<T> list,Func<T, T, int> comparer)
    {
        if (list.Count == 0) return default(T);
        var res = list[0];
        foreach (var item in list)
        {
            if (comparer(item, res) < 0)
                res = item;
        }
        return res;
    }
    public static T GetMaxItem<T>(this List<T> list, Func<T, T, int> comparer)
    {
        if (list.Count == 0) return default(T);
        var res = list[0];
        foreach (var item in list)
        {
            if (comparer(item, res) > 0)
                res = item;
        }
        return res;
    }
}
