using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSummonDerive : NoTargetCardEffect
{
    string deriveName;
    public RandomSummonDerive(Card card,string name) : base(card)
    {
        this.deriveName = name;
    }
    public override string ToString()
    {
        return $"在随机位置召唤一个{deriveName}。";
    }
    public override bool CanUse()
    {
        return CellManager.Instance.GetCells().FindAll(c => c.CanSummon()).Count > 0;
    }
    public override void Excute()
    {
        var cell = CellManager.Instance.GetCells().FindAll(c => c.CanSummon()).GetRandomItem();
        var info=new CardInfo() { name = deriveName};
        var card = CardStore.Instance.CreateCard(info);
        card.field.Summon(cell);
    }
}
