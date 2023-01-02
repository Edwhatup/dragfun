using System;
using System.Collections.Generic;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class RequireCardComponentAttribute : Attribute
{
    public Type type;
    public RequireCardComponentAttribute(Type type)
    {
        this.type = type;
    }
    static Dictionary<Type, List<Type>> requireTypes;
    private static void GetTypes()
    {
        if (requireTypes == null)
            requireTypes = new Dictionary<Type, List<Type>>();
        requireTypes.Clear();
        var ass = typeof(CardComponent).Assembly;
        foreach (var t in ass.GetTypes())
        {
            if (t.IsSubclassOf(typeof(CardComponent)))
            {
                var attri = t.GetCustomAttributes(typeof(RequireCardComponentAttribute), true);
                if (attri.Length > 0)
                {
                    requireTypes[t] = new List<Type>();
                    foreach (RequireCardComponentAttribute attr in attri)
                    {
                        if (attr.type != t)
                            requireTypes[t].Add(attr.type);
                    }
                }
            }
        }
    }
    private static List<Type> GetPreComponentTypes(Type type)
    {
        if (requireTypes == null) GetTypes();
        if (requireTypes.ContainsKey(type)) return requireTypes[type];
        else return null;
    }
    public static List<CardComponent> GetPreComponents(Type type)
    {
        var t = GetPreComponentTypes(type);
        var res = new List<CardComponent>();
        if (t == null) return res;
        foreach (var c in t)
        {
            var ctor = c.GetConstructor(Type.EmptyTypes);
            res.Add(ctor.Invoke(null) as CardComponent);    
        }
        return res;
    }

}
