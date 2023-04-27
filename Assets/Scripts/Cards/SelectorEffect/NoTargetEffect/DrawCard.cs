using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 根据场上攻击次数给周围8格的随从上BUFF
/// </summary>
public class DrawCard : NoTargetCardEffect
{
    int drawCnt;
    public DrawCard(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out drawCnt);
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public DrawCard(Card card, int drawCnt) : base(card)
    {
        this.drawCnt = drawCnt;
    }
    public override void Excute()
    {
        CardManager.Instance.DrawCard(drawCnt);

    }

    public override string ToString()
    {
        return $"抽{drawCnt}张牌";
    }
}