using System.Collections.Generic;

public class SummonComponent : CardComponent, ISelector
{
    Ref<int> cost;
    Ref<int> isFree;
    public int Cost => isFree.value <= 0 && cost.value > 0 ? cost.value : 0;
    public int TargetCount => 1;
    public CardEffect effect;
    public IReadOnlyList<CardTarget> CardTargets => new List<CardTarget>() { CardTarget.Cell };

    public List<ISeletableTarget> Targets => targets;
    List<ISeletableTarget> targets = new List<ISeletableTarget>();
    public SummonComponent(int cost, CardEffect effect = null)
    {
        this.cost = new Ref<int>(cost);
        this.isFree = new Ref<int>(1);
    }
    public void Summon(Cell targetCell, bool active)
    {
        int cost = active ? Cost : 0;
        var se = new SummonEvent(card, targetCell);
        var ue = new UseEvent(card, cost);
        EventManager.Instance.PassEvent(se);
        EventManager.Instance.PassEvent(ue);
        targetCell.Summon(card);
        CardManager.Instance.SummonCard(card);
        EventManager.Instance.PassEvent(se.EventAfter());
        EventManager.Instance.PassEvent(ue.EventAfter());
    }

    public virtual bool CanUse()
    {
        return GameManager.Instance.pp >= Cost && CellManager.Instance.GetAllSpecifyCells((e) => e.CanSummon()).Count > 0;
    }

    public virtual bool CanSelectTarget(ISeletableTarget target, int i)
    {
        if (target is Cell cell)
        {
            return cell.CanSummon();
        }
        return false;
    }
    public override void Init()
    {
        if (effect != null)
            effect.card = card;
    }
    public void Excute()
    {
        var cell = targets[0] as Cell;
        Summon(cell, true);
        card.visual.SetRayCastTarget(true);
    }

    public ISelector GetNextSelector()
    {
        var cell = targets[0] as Cell;
        if (effect != null && effect.CanUse())
        {
            cell.PreShowCard(card);
            return effect;
        }
        return null;
    }

    public void CancleSelect()
    {
        card.visual.SetRayCastTarget(true);
        card.state = CardState.InHand;
        Targets.Clear();
    }

    public void OnSelected()
    {
        Selections.Instance.mouseFollowObject = card.visual.gameObject;
        card.visual.SetRayCastTarget(false);
        card.state = CardState.PreUse;
        card.visual.transform.SetParent(Selections.Instance.selectEleParent);
    }
    public override string Desc()
    {
        if (effect != null)
        {
            return "召唤时：" + effect.ToString() + "。";
        }
        return "";
    }

    public override void Add(CardComponent component)
    {
        throw new System.Exception("不能重复添加召唤组件");
    }
}