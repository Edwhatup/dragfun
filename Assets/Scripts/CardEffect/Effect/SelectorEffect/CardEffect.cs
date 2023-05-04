using System.Collections.Generic;
using System;
public abstract class CardEffect :  ISelector
{
    public Card card;
    public static Dictionary<string, Type> CardEffects;
    static CardEffect()
    {
        CardEffects = new Dictionary<string, Type>();
        var assembly = typeof(CardEffect).Assembly;
        foreach (var type in assembly.GetTypes())
        {
            if (!type.IsAbstract && type.IsSubclassOf(typeof(CardEffect)))
                CardEffects.Add(type.Name, type);
        }
    }
    public CardEffect(Card card)
    {
        this.card = card;
        InitTarget();   
    }
    public abstract void InitTarget();
    public int TargetCount { get; protected set; }
    public List<CardTarget> CardTargets { get; }=new List<CardTarget>(){ };
    public List<ISeletableTarget> Targets { get ; }= new List<ISeletableTarget>();

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
        Targets.Clear();
    }
    public virtual void OnSelected()
    {
        return;
    }
}


public abstract class NoTargetCardEffect : CardEffect
{
    protected NoTargetCardEffect(Card card) : base(card)
    {
    }
    public override void InitTarget()
    {
        TargetCount = 0;
    }
}
