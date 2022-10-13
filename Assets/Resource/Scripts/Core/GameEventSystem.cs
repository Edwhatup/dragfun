using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem
{
    public static void CallInterfaceMethod<I>(object o,params object[] args)where I :class
    {
        I i=o as I;
        if(i!=null)
        {
            var methods=typeof(I).GetMethods();
            if (methods.Length > 0)
            {
                var method=methods[0];
                var paras=method.GetParameters();
                if (args.Length != paras.Length)
                {
                    Debug.LogError($"调用{o.GetType().Name}类型实现的{typeof(I).Name}接口方法是参数数量不匹配，期望{args.Length},实际{paras.Length}");
                    return;
                }
                for (int k = 0; k < paras.Length; k++)
                {
                    var paraType=paras[k].GetType();
                    var argsType=args[k].GetType();
                    if(!(argsType == paraType || argsType.IsSubclassOf(paraType)))
                    {
                        Debug.LogError($"第{k}个参数类型不匹配，期望{paraType.Name},实际{argsType.Name}。");
                        return;
                    }
                }
                methods[0].Invoke(o, args);
            }
        }
    }
}
