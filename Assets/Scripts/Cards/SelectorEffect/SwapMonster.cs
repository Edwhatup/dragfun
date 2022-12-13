using System.Collections.Generic;
public class SwapMonster : CardEffect
{
    public SwapMonster(string[] paras):base()
    {
    }
    public SwapMonster():base()
    {
        InitTarget();
    }

    public override bool CanUse()
    {
        return CardManager.Instance.GetAllCardsOnBoard().Count >= 2;
    }

    public override void Excute()
    {
        var visual1 = Targets[0] as CardVisual;
        var visual2 = Targets[1] as CardVisual;
        var cell1 = visual1.card.field.cell;
        visual2.card.field.cell.Summon(visual1.card);
        cell1.Summon(visual2.card);
    }

    public override void OnSelected()
    {
        UnityEngine.Debug.Log(card);
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string Desc()
    {
        return "交换两个随从的位置";
    }

    public override void InitTarget()
    {
        TargetCount = 2;
        CardTargets = new List<CardTarget>() { CardTarget.MonsterOnBoard, CardTarget.MonsterOnBoard };
    }
}