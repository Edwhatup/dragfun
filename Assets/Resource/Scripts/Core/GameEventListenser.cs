using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Core.GameEvent
{
    public delegate void GameActionListen(AbstractAction args);
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionListenerAttribute : Attribute
    {
        public static GameActionListen GetListener(object o)
        {
            Type[] types = new Type[] { typeof(int) };
            Type type= o.GetType();
            var methods=type.GetMethods();
            foreach (var m in methods)
            {
                var attributes = m.GetCustomAttributes(typeof(ActionListenerAttribute), false);
                if (attributes.Length > 0)
                {
                    var attribute = attributes[0];
                    var listenMethod = type.GetMethod("GameEventListen", new Type[] { typeof(AbstractAction) });
                    return (GameActionListen)listenMethod?.CreateDelegate(typeof(GameActionListen), o);
                }
            }
            return null;
        }
    }
}
