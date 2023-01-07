using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对指定友方增加攻击范围
/// </summary>
public class AddAtkRange : CardEffect
{
    int extraRange;
    public AddAtkRange(string[] paras)
    {
        int.TryParse(paras[0], out extraRange);
        InitTarget();
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public AddAtkRange(int extraRange)
    {
        this.extraRange = extraRange;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets = new List<CardTarget>() {CardTarget.Monster};
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