using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseComponent : CardComponent,ISelector
{
    int consume;
    public bool ConSume => consume > 0;
    ISelector selector;
    public int cost;
    public bool IsFree => isFree > 0;
    int isFree;
    public List<ISeletableTarget> Targets => selector.Targets;

    public int TargetCount => selector.TargetCount;

    public IReadOnlyList<CardTarget> CardTargets => selector.CardTargets;
    private UseComponent(int cost, bool isFree = false)
    {
        this.cost = cost;
        this.isFree = isFree ? 1 : 0;
    }


    public UseComponent(SpellCastComponent cast, int cost, bool isFree = false):this(cost,isFree)
    {
        this.selector = cast;
    }
    public UseComponent(SummonComponent summon, int cost, bool isFree = false) : this(cost, isFree)
    {
        this.selector = summon;
    }
    public UseComponent(SpellCastComponent cast, int cost,bool consume, bool isFree = false) : this(cast,cost, isFree)
    {
        this.consume = consume ? 1 : 0;
    }
    public UseComponent(SummonComponent summon, int cost,bool consume, bool isFree = false) : this(summon,cost, isFree)
    {
        this.consume = consume ? 1 : 0;
    }
    public bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return selector.CanSelectTarget(target, i);
    }

    public bool CanUse()
    {
        return cost <= GameManager.Instance.pp && selector.CanUse();
    }

    public void Excute()
    {
        int ppCost = IsFree ? cost : 0;
        var bue = new BeforeUseEvent(card);
        EventManager.Instance.PassEvent(bue);
        if (bue.isCounter) return;
        selector.Excute();
        CardManager.Instance.UseCard(card);
        GameManager.Instance.pp -= ppCost;
        var aue = new AfterUseEvent(card,cost);
        EventManager.Instance.PassEvent(aue);
    }

    public ISelector GetNextSelector()
    {
        return selector.GetNextSelector();
    }

    public void CancleSelect()
    {
        card.visual.SetRayCastTarget(true);
        Targets.Clear();
    }

    public void OnSelected()
    {
        Targets.Clear();
        Selections.Instance.mouseFollowObject = card.visual.gameObject;
        card.visual.SetRayCastTarget(false);
        card.visual.transform.SetParent(Selections.Instance.selectEleParent);
    }
}
