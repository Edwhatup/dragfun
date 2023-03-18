using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAtkByDerNumber : NoTargetCardEffect
{
    int baseBuff;
    public BuffAtkByDerNumber(Card card,int baseBuff) : base(card)
    {
        this.baseBuff = baseBuff;
    }
    public override string ToString()
    {
        return $"场上每有一个它的衍生物，就提升{baseBuff}点攻击力";
    }
    public override void Excute()
    { 
        var cell = card.field.cell;
        var cells = CellManager.Instance.GetCells().FindAll(c => c.card.type==CardType.FriendlyDerive);
        foreach (var c in cells)
        {
            card.Buff(card,baseBuff,0);
        }
    }
}