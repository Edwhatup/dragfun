using System.Collections.Generic;
/// <summary>
/// 强化随从
/// </summary>
public class StrengthMonsterEffect : CardEffect
{
    int hpModifier;
    int atkMofifier;
    public StrengthMonsterEffect(string[] paras):base()
    {
        int.TryParse(paras[0], out hpModifier);
        int.TryParse(paras[1], out atkMofifier);
    }
    public StrengthMonsterEffect(int hpModifier, int atkMofifier) : base()
    {
        this.hpModifier = hpModifier;
        this.atkMofifier = atkMofifier;
    }

    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets = new List<CardTarget>() { CardTarget.MonsterOnBoard };
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

    public override string Desc()
    {
        return "";
    }
}