using System.Collections.Generic;
/// <summary>
/// 强化随从
/// </summary>
public class SelfBuffEffect : NoTargetCardEffect
{
    int hpModifier;
    int atkMofifier;
    public SelfBuffEffect(Card card, string[] paras) :base(card)
    {
        int.TryParse(paras[0], out hpModifier);
        int.TryParse(paras[1], out atkMofifier);
    }
    public SelfBuffEffect(Card card, int hpModifier, int atkMofifier):base(card)
    {
        this.hpModifier = hpModifier;
        this.atkMofifier = atkMofifier;
    }

    public override string ToString()
    {
        if (atkMofifier == 0) return $"获得+{atkMofifier}攻击力";
        if (hpModifier == 0) return $"获得+{hpModifier}生命值";
        return $"获得+{atkMofifier}+{hpModifier}";
    }
    public override void Excute()
    {
        card.Buff(card, atkMofifier, hpModifier);
    }
}