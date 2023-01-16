using System.Collections.Generic;
using UnityEngine;

[CanRepeat(false)]
[RequireCardComponent(typeof(UseComponent))]
public class SummonComponent : CardComponent, ISelector
{
    public int TargetCount => 1;
    public CardEffect effect;
    public List<CardTarget> CardTargets { get; } = new List<CardTarget>() { CardTarget.Cell };
    public List<ISeletableTarget> Targets { get; } = new List<ISeletableTarget>();
    public SummonComponent(CardEffect effect = null)
    {
        this.effect = effect;

    }
    public void Summon(Cell targetCell, bool active)
    {
        card.field.Summon(targetCell);
    }

    public virtual bool CanUse()
    {
        return CellManager.Instance.GetAllSpecifyCells((e) => e.CanSummon()).Count >=  0;
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
        var cell = Targets[0] as Cell;
        Summon(cell, true);
        card.visual.SetRayCastTarget(true);
    }

    public ISelector GetNextSelector()
    {
        var cell = Targets[0] as Cell;
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
    public override string ToString()
    {
        if(effect != null)
        {
            return $"战吼：{effect.ToString()}。";
        }
        return "";
    }
    public void OnSelected()
    {

    }
}