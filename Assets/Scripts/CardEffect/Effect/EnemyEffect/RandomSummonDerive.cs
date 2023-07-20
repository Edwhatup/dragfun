
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSummonDerive : NoTargetCardEffect
{
    List<string> deriveNames = null;
    string deriveName = null;
    int count;
    public RandomSummonDerive(Card card, string name, int count) : base(card)
    {
        this.deriveName = name;
        this.count = count;
    }
    public RandomSummonDerive(Card card, List<string> names, int count) : base(card)
    {
        this.deriveNames = names;
        this.count = count;
    }
    public override string ToString()
    {
        if (deriveNames != null)
        {
            return $"在随机位置召唤{count}个它的衍生物。";
        }
        return $"在随机位置召唤{count}个{deriveName}。";
    }
    public override bool CanUse()
    {
        return CellManager.Instance.GetCells().FindAll(c => c.CanSummon()).Count > 0;
    }
    public void SingleSummon(string name, int count)
    {
        for (int i = 0; i < count; i++)
        {
            var cell = CellManager.Instance.GetCells().FindAll(c => c.CanSummon()).GetRandomItem();
            var info = new CardInfo() { name = name };
            var card = CardStore.Instance.CreateCard(info);
            card.field.Summon(cell);
            // CardManager.Instance.drawDeck.Transfer(CardManager.Instance.board, card);
        }
    }
    public override void Excute()
    {
        if (deriveName != null)
        {
            SingleSummon(deriveName, count);
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                string name = deriveNames[Random.Range(0, deriveNames.Count)];
                SingleSummon(name, 1);
            }
        }
    }
}
