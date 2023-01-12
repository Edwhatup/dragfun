using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 根据场上攻击次数给周围8格的随从上BUFF
/// </summary>
public class BuffByAtkCount : CardEffect
{
    int extraRange;
    public BuffByAtkCount(string[] paras)
    {
        int.TryParse(paras[0], out extraRange);
        InitTarget();
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public BuffByAtkCount(int extraRange)
    {
        this.extraRange = extraRange;
        InitTarget();
    }
    public override void InitTarget()
    {
        NoTarget();
    }
    public override void OnSelected()
    {
        return;
    }
    public override void Excute()
    {
        var monster =(Targets[0] as CardVisual).card;
        monster.attack.RangeUp(card, extraRange);
    }

    public override string ToString()
    {
        return $"使一名友方随从获得增程{extraRange}";
    }
}