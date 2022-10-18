using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace CardEvent
{
    public delegate void CardEventListen(object cardEvent);
    //添加了该属性的方法将被添加到卡牌事件监听列表中
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class EventListenerAttribute : Attribute
    {
        public static List<CardEventListen> GetListener(object o)
        {
            Type[] types = new Type[] { typeof(int) };
            Type type= o.GetType();
            var methods=type.GetMethods();
            var res=new List<CardEventListen>();
            foreach (var m in methods)
            {
                var attributes = m.GetCustomAttributes(typeof(EventListenerAttribute), false);
                if (attributes.Length > 0)
                {
                    res.Add(m.CreateDelegate(typeof(CardEventListen), o) as CardEventListen);
                }
            }
            return res;
        }
    }
}
