using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 随机抽一张指定种族等条件的随从
/// </summary>
public class RequireRandomDraw : NoTargetCardEffect
{
    int drawCnt;
    CardRace race;
    public RequireRandomDraw(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out drawCnt);
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public RequireRandomDraw(Card card, int drawCnt,CardRace race) : base(card)
    {
        this.drawCnt = drawCnt;
        this.race = race;
    }
    public override void Excute()
    {
        CardManager.Instance.DrawSpecificRaceCard(drawCnt,race);

    }

    public override string ToString()
    {
        return $"将{drawCnt}张埴轮随从抽入手牌";
    }
}