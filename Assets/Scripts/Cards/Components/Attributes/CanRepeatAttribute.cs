using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class CanRepeatAttribute : Attribute
{
    public bool canRepeat;

    public CanRepeatAttribute(bool canRepeat)
    {
        this.canRepeat = canRepeat;
    }
    static Dictionary<string, bool> RepeatDic;

    public static bool CanRepeat<T>(T t) where T : CardComponent
    {
        var type = t.GetType();
        if (RepeatDic == null)
            RepeatDic = new Dictionary<string, bool>();
        if (!RepeatDic.ContainsKey(type.Name))
        {
            var attributes = type.GetCustomAttributes(typeof(CanRepeatAttribute), false);
            if (attributes.Length > 0)
            {
                var a = attributes[0] as CanRepeatAttribute;
                RepeatDic[type.Name] = a.canRepeat;
            }
            else return false;
        }
        return RepeatDic[type.Name];
    }
}
