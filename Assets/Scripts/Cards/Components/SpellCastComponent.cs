using System.Collections.Generic;
[CanRepeat(false)]
public class SpellCastComponent : CardComponent, ISelector
{
    int consume;
    int wreckAge;
    public int TargetCount => 1;
    bool Consume => consume > 0;
    public CardEffect effect;
    public IReadOnlyList<CardTarget> CardTargets => new List<CardTarget>() { CardTarget.Cell };

    public List<ISeletableTarget> Targets => targets;
    List<ISeletableTarget> targets = new List<ISeletableTarget>();
    public SpellCastComponent(Card card, int wreckAge, CardEffect effect, bool consume = false)
    {
        this.card = card;
        this.wreckAge = wreckAge;
        this.consume = consume ? 1 : 0;
        this.effect = effect;
        effect.card = card;
    }
    public void Cast(Cell targetCell, bool active)
    {
        var wreck = new SpellWreck(wreckAge);
        CardStore.Instance.CreateCardVisual(wreck);
        wreck.camp = CardCamp.Friendly;
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
            return cell.CanCastSpell();
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
        var cell = targets[0] as Cell;
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
