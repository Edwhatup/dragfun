using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseComponent : CardComponent, ISelector
{
    int consume;
    public bool ConSume => consume > 0;
    ISelector selector;
    public int cost;
    public bool IsFree => isFree > 0;
    int isFree;
    public List<ISeletableTarget> Targets => selector.Targets;
    public int TargetCount => selector.TargetCount;
    public List<CardTarget> CardTargets => selector.CardTargets;
    public UseComponent(int cost, bool consume = false, bool isFree = false)
    {
        this.cost = cost;
        this.isFree = isFree ? 1 : 0;
        this.consume = consume ? 1 : 0;
    }
    public UseComponent()
    {
        this.cost = 0;
        this.isFree = 0;
        this.consume = 0;
    }
    public override void Init()
    {
        if (card.GetComponent<SpellCastComponent>() != null) selector = card.GetComponent<SpellCastComponent>();
        else if (card.GetComponent<SummonComponent>() != null) selector = card.GetComponent<SummonComponent>();
        else throw new System.Exception("组件不完整");
        this.consume = card.consume;
        this.cost = card.cost;
    }
    public bool CanSelectTarget(ISeletableTarget target, int i)
    {
        return selector.CanSelectTarget(target, i);
    }

    public bool CanUse()
    {
        return cost <= GameManager.Instance.Pp && selector.CanUse();
    }

    public void Excute()
    {
        int ppCost = !IsFree ? cost : 0;
        if (!GameManager.Instance.TryCostPP(ppCost)) return;

        var bue = new BeforeUseEvent(card);
        EventManager.Instance.PassEvent(bue);
        if (bue.isCounter) return;
        selector.Excute();
        CardManager.Instance.UseCard(card);
        // GameManager.Instance.Pp -= ppCost;
        var aue = new AfterUseEvent(card, cost);
        EventManager.Instance.PassEvent(aue);
    }

    public ISelector GetNextSelector()
    {
        return selector.GetNextSelector();
    }

    public void CancleSelect()
    {
        if (Targets.Count > 0)
        {
            var cell = Targets[0] as Cell;
            cell.preShowCard = null;
        }
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
