using System.Collections.Generic;
public abstract class CardSelector : ISelector
{
    public Card card;
    protected List<CardTarget> cardTargets;
    protected int targetCount;
    public IReadOnlyList<CardTarget> CardTargets => cardTargets;
    public int TargetCount => targetCount;
    protected List<ISeletableTarget> targets=new List<ISeletableTarget>();
    public List<ISeletableTarget> Targets => targets;

    public virtual void CancleSelect()
    {
        return;
    }

    public virtual bool CanSelectTarget(ISeletableTarget target, int i)
    {
        if (target is CardVisual cardVisual)
        {
            return cardVisual != card.visual;
        }
        return true;
    }

    public abstract bool CanUse();
    public abstract void Excute();

    public virtual ISelector GetNextSelector()
    {
        return null;
    }

    public abstract void OnSelected();
}