using System.Collections.Generic;
/// <summary>
/// 强化随从
/// </summary>
public class GetTaunt : NoTargetCardEffect
{
    int hpModifier;
    int atkMofifier;
    public GetTaunt(Card card, string[] paras) :base(card)
    {
        
    }
    public GetTaunt(Card card):base(card)
    {
       
    }

    public override string ToString()
    {
        return $"嘲讽";
    }
    public override void Excute()
    {
        var buff = new TauntBuff();
        card.AddBuff(buff);
    }
}