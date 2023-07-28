using System.Collections.Generic;
public class SwapMonster : CardEffect
{
    public SwapMonster(Card card, string[] paras):base(card)
    {
    }
    public SwapMonster(Card card):base(card)
    {
    }

    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly).Count >= 2;
    }

    public override void Excute()
    {
        var visual1 = Targets[0] as CardVisual;
        var visual2 = Targets[1] as CardVisual;
        var cell1 = visual1.card.field.cell;
        visual2.card.field.cell.PlaceCard(visual1.card);
        cell1.PlaceCard(visual2.card);
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return "交换两个随从的位置";
    }

    public override void InitTarget()
    {
        TargetCount = 2;
        CardTargets.Add(CardTarget.Monster | CardTarget.FriendlyDerive);
        CardTargets.Add(CardTarget.Monster | CardTarget.FriendlyDerive);
    } 
}