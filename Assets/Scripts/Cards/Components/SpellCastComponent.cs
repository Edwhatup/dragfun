using System.Collections.Generic;

public class SpellCastComponent : CardComponent, ISelector
{
    Ref<int> consume;
    Ref<int> cost;
    Ref<int> isFree;
    Ref<int> wreckAge;
    public int PPCost => isFree.value <= 0 && cost.value > 0 ? cost.value : 0;
    public int WreckAge => wreckAge.value;
    public int TargetCount => 1;
    public bool Consume => consume.value > 0;
    public CardEffect effect;
    public IReadOnlyList<CardTarget> CardTargets => new List<CardTarget>() { CardTarget.Cell };

    public List<ISeletableTarget> Targets => targets;
    List<ISeletableTarget> targets = new List<ISeletableTarget>();
    public SpellCastComponent(int cost, int wreckAge, CardEffect effect, bool consume = false)
    {
        this.cost = new Ref<int>(cost);
        this.isFree = new Ref<int>(0);
        this.wreckAge = new Ref<int>(wreckAge);
        this.consume = new Ref<int>(consume ? 1 : 0);
        this.effect = effect;
    }
    public void Cast(Cell targetCell, bool active)
    {
        int cost = active ? PPCost : 0;
        var ue = new UseEvent(card, cost);
        EventManager.Instance.PassEvent(ue);

        var wreck = CardStore.Instance.CreateCard("法术残骸", new string[] { WreckAge.ToString() });
        var wreckCard = wreck.GetComponent<CardVisual>().card;
        wreckCard.camp = CardCamp.Friendly;
        wreckCard.source = card;
        CardManager.Instance.UseSpell(card);
        EventManager.Instance.PassEvent(ue.EventAfter());

        var sc = new SummonEvent(wreckCard, targetCell);
        EventManager.Instance.PassEvent(sc);
        targetCell.Summon(wreckCard);
        CardManager.Instance.SummonCard(wreckCard);
        EventManager.Instance.PassEvent(sc.EventAfter());

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
        return GameManager.Instance.pp >= PPCost && CellManager.Instance.GetAllSpecifyCells((e) => e.CanSummon()).Count > 0 && effect.CanUse();
    }

    public void Excute()
    {
        var target = Targets[0] as Cell;
        Cast(target, true);
        card.visual.SetRayCastTarget(true);
    }

    public ISelector GetNextSelector()
    {
        var cell = targets[0] as Cell;
        cell.PreShowCard(card);
        return effect;
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
        card.state = CardState.PreUse;
        card.visual.SetRayCastTarget(false);
        card.visual.transform.SetParent(Selections.Instance.selectEleParent);
    }
    public override string Desc()
    {
        if (effect != null)
        {
            return effect.Desc() + "。";
        }
        return "";
    }

    public override void Add(CardComponent component)
    {
        throw new System.Exception("不能添加两个相同的释放组件");
    }
}
