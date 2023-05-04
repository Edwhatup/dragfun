using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 根据场上攻击次数给周围8格的随从上BUFF
/// </summary>
public class BuffByAtkCount : NoTargetCardEffect
{
    int extraRange;
    public BuffByAtkCount(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out extraRange);
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public BuffByAtkCount(Card card, int extraRange) : base(card)
    {
        this.extraRange = extraRange;
    }
    public override void Excute()
    {
        var monster =(Targets[0] as CardVisual).card;
        card.AddBuff(new RangeUpBuff(extraRange));
    }

    public override string ToString()
    {
        return $"使一名友方随从获得增程{extraRange}";
    }
}