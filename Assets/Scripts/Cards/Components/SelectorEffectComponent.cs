using System.Collections.Generic;
/// <summary>
/// 具有选择器的卡牌效果组件，用作法术牌的效果和随从牌的召唤效果
/// </summary>
public abstract class SelectorEffectComponent : CardComponent,ISelector
{
    public int TargetCount { get;private set; }
    public IReadOnlyList<CardTarget> CardTargets { get; private set; }

    public List<ISeletableTarget> Targets => targets;
    protected List<ISeletableTarget> targets=new List<ISeletableTarget>();  
    public virtual void CancleSelect()
    {
        return;
    }

    public virtual bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return true;
    }
    public abstract bool CanUse();

    public virtual ISelector GetNextSelector()
    {
        return null;
    }

    public abstract void OnSelected();
    public abstract void Excute();
}