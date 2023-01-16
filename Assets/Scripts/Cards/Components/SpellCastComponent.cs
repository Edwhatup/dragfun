using System.Collections.Generic;
[CanRepeat(false)]
[RequireCardComponent(typeof(UseComponent))]
public class SpellCastComponent : CardComponent, ISelector
{
    int wreckAge;
    public int TargetCount => 1;
    public CardEffect effect;
    public List<CardTarget> CardTargets => new List<CardTarget>() { CardTarget.Cell };
    public List<ISeletableTarget> Targets { get; }=new List<ISeletableTarget>(){ };
    public SpellCastComponent(int wreckAge, CardEffect effect)
    {
        this.wreckAge = wreckAge;
        this.effect = effect;
    }
    public void Cast(Cell targetCell, bool active)
    {
        var info = new CardInfo()
        {
            name = "法术残骸",
            paras = new string[] { wreckAge.ToString() }
        };
        var wreck = CardStore.Instance.CreateCard(info);
        CardStore.Instance.CreateCardVisual(wreck);
        wreck.source = card;
        wreck.field.Summon(targetCell);
    }
    public override void Init()
    {
        if (effect != null)
            effect.card = card;
    }
    public virtual bool CanSelectTarget(ISeletableTarget target, int i)
    {
        if (target is Cell cell)
            return cell.CanSummon();
        return false;
    }

    public bool CanUse()
    {
        return CellManager.Instance.GetAllSpecifyCells((e) => e.CanSummon()).Count > 0 && effect.CanUse();
    }

    public void Excute()
    {
        var target = Targets[0] as Cell;
        Cast(target, true);
    }

    public ISelector GetNextSelector()
    {
        var cell = Targets[0] as Cell;
        cell.PreShowCard(card);
        return effect;
    }

    public void CancleSelect()
    {
    }

    public void OnSelected()
    {

    }
}
