using System.Collections.Generic;
using System;

public abstract class CardEffect : CardComponent, ISelector
{
    public static Dictionary<string, Type> CardEffects;
    static CardEffect()
    {
        CardEffects = new Dictionary<string, Type>();
        var assembly = typeof(CardEffect).Assembly;
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsSubclassOf(typeof(CardEffect)))
                CardEffects.Add(type.Name, type);
        }
    }
    public CardEffect()
    {
        InitTarget();   
    }
    public abstract void InitTarget();
    public void NoTarget()
    {
        TargetCount = 0;
        CardTargets = new List<CardTarget>();
    }
    public int TargetCount { get; protected set; }

    public IReadOnlyList<CardTarget> CardTargets { get; protected set; }


    List<ISeletableTarget> targets = new List<ISeletableTarget>();
    public List<ISeletableTarget> Targets { get => targets; }

    public static CardEffect GetCardEffect(string effectName, string[] paras)
    {
        var ctor = CardEffects[effectName].GetConstructor(new Type[] { typeof(string[]) });
        return ctor.Invoke(paras) as CardEffect;
    }

    public virtual bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return true;
    }

    public virtual bool CanUse()
    {
        return true;
    }

    public abstract void Excute();

    public virtual ISelector GetNextSelector()
    {
        return null;
    }
    public virtual void CancleSelect()
    {
        return;
    }
    public override void Add(CardComponent component)
    {
        throw new Exception("不支持的操作");
    }
    public abstract void OnSelected();
}