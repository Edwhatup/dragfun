using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 根据入场次数给周围8格的随从上BUFF
/// </summary>
public class CreatureRainforce : NoTargetCardEffect
{
    int buffCnt;
    Card target;
    public CreatureRainforce(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out buffCnt);
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public CreatureRainforce(Card card, Card target, int buffCnt) : base(card)
    {
        this.target = target;
        this.buffCnt = buffCnt;
    }
    public override void Excute()
    {
        for(int a=1;a<=buffCnt;++a)
        {
            int i= Random.Range(1,6);
            switch (i)
            {
                case 1:
                    target.AddBuff(new StatsPositiveBuff(3,3));
                    break;
                case 2:
                    target.AddBuff(new RangeUpBuff(1));
                    break;
                case 3:
                    target.AddBuff(new BlessBuff());
                    break;
                case 4:
                    target.AddBuff(new SwiftBuff());
                    break;
                case 5:
                    target.AddBuff(new TauntBuff());
                    break;           
            }
        }
            
    }

    public override string ToString()
    {
        return $"进行{buffCnt}次造型强化";
    }
}