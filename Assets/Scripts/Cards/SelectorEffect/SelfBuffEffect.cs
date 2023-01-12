using System.Collections.Generic;
/// <summary>
/// 强化随从
/// </summary>
public class SelfBuffEffect : CardEffect
{
    int hpModifier;
    int atkMofifier;
    public SelfBuffEffect(string[] paras) 
    {
        int.TryParse(paras[0], out hpModifier);
        int.TryParse(paras[1], out atkMofifier);
        InitTarget();
    }
    public SelfBuffEffect(int hpModifier, int atkMofifier)
    {
        this.hpModifier = hpModifier;
        this.atkMofifier = atkMofifier;
        InitTarget();
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
    public override void InitTarget()
    {
        NoTarget();
    }


    public override void OnSelected()
    {

    }

}