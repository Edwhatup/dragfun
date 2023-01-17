using System.Collections.Generic;
/// <summary>
/// 强化随从
/// </summary>
public class PermanentStrengthMonster : CardEffect
{
    int hpModifier;
    int atkMofifier;
    public PermanentStrengthMonster(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out hpModifier);
        int.TryParse(paras[1], out atkMofifier);
    }
    public PermanentStrengthMonster(Card card, int hpModifier, int atkMofifier) : base(card)
    {
        this.hpModifier = hpModifier;
        this.atkMofifier = atkMofifier;
    }

    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.Monster);
    }

    public override void Excute()
    {
        var monster = (Targets[0] as CardVisual).card;
        //card.AddBuff("hp",hpModifier);
        //card.AddBuff("atk", hpModifier);
        //BattleManager.Buff(card, monster, hpModifier, atkMofifier);
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return "";
    }
}