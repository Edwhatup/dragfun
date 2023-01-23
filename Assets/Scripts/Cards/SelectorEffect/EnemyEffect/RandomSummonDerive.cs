using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSummonDerive : NoTargetCardEffect
{
    string deriveName;
    int count;
    public RandomSummonDerive(Card card,string name,int count) : base(card)
    {
        this.deriveName = name;
        this.count=count;
    }
    public override string ToString()
    {
        return $"在随机位置召唤{count}个{deriveName}。";
    }
    public override bool CanUse()
    {
        return CellManager.Instance.GetCells().FindAll(c => c.CanSummon()).Count > 0;
    }
    public override void Excute()
    {
        for(int i=0;i<count;i++)
        {
            var cell = CellManager.Instance.GetCells().FindAll(c => c.CanSummon()).GetRandomItem();
            var info=new CardInfo() { name = deriveName};
            var card = CardStore.Instance.CreateCard(info);
            card.field.Summon(cell);
        }
    }
}
