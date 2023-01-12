using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对指定友方增加共鸣：+1/+1
/// </summary>
public class AddResonanceBuff : CardEffect
{
    public AddResonanceBuff(string[] paras)
    {
        InitTarget();
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public AddResonanceBuff()
    {
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
        var e =new SelfBuffEffect(1,1);
        var r =new ResonanceComponent(e);
        monster.AddComponnet(r);
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return $"使一名友方随从获得共鸣：+1/+1";
    }
}