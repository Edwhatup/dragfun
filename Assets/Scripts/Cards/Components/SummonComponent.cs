using System.Collections.Generic;
using UnityEngine;

[CanRepeat(false)]
public class SummonComponent : CardComponent, ISelector
{
    public int TargetCount => 1;
    public CardEffect effect;
    public IReadOnlyList<CardTarget> CardTargets => new List<CardTarget>() { CardTarget.Cell };

    public List<ISeletableTarget> Targets => targets;
    List<ISeletableTarget> targets = new List<ISeletableTarget>();
    public SummonComponent(Card card, CardEffect effect = null)
    {
        this.card = card;
        if(effect!=null) effect.card = card;
        this.effect = effect;

    }
    public void Summon(Cell targetCell, bool active)
    {
        card.field.Summon(targetCell);
    }

    public virtual bool CanUse()
    {
        return CellManager.Instance.GetAllSpecifyCells((e) => e.CanSummon()).Count > 0;
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

    }

    public void OnSelected()
    {

    }
}