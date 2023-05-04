using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 根据场上攻击次数给周围8格的随从上BUFF
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
                    break;
                case 4:
                    break;
                case 5:
                    target.GetComponent<AttackedComponent>().taunt=1;
                    break;           
            }
        }
            
    }

    public override string ToString()
    {
        return $"抽{buffCnt}张牌";
    }
}