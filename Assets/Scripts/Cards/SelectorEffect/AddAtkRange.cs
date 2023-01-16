using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对指定友方增加攻击范围
/// </summary>
public class AddAtkRange : CardEffect
{
    int extraRange;
    public AddAtkRange(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out extraRange);
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.Has(c => c.camp == CardCamp.Friendly && c.field.state == BattleState.Survive);
        //return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public AddAtkRange(Card card, int extraRange):base(card)
    {
        this.extraRange = extraRange;
    }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.Monster);
    }
    public override void Excute()
    {
        var monster =(Targets[0] as CardVisual).card;
        monster.attack.RangeUp(card, extraRange);
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return $"使一名友方随从获得增程{extraRange}";
    }
}