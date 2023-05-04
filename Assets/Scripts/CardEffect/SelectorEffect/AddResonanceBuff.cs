using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对指定友方增加共鸣：+1/+1
/// </summary>
public class AddResonanceBuff : CardEffect
{
    public AddResonanceBuff(Card card, string[] paras):base(card)
    {}
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public AddResonanceBuff(Card card):base(card)
    { }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.Monster) ;
    }
    public override void Excute()
    {
        var monster =(Targets[0] as CardVisual).card;
        var e =new SelfBuffEffect(monster,1,1);
        monster.AddComponnet(new ResonanceComponent(e));
        //var r =new ResonanceComponent(e);
        //monster.AddComponnet(r);
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