using System;
using System.Collections;
using System.Collections.Generic;

public class LifeEnergyCost : NoTargetCardEffect
{
    int cost;
    CardEffect effect;

    public LifeEnergyCost(Card card, int cost, CardEffect effect) : base(card)
    {
        this.cost = cost;
        this.effect = effect;
    }

    //public override string ToString()
    //{
    //    return $"生命能量{cost}："+effect.ToString();
    //}

    public override void Excute()
    {
        if (GameManager.Instance.LifeEnergyPoint >= cost)
        {
            GameManager.Instance.LifeEnergyPoint -= cost;
            effect.Excute();
        }
    }
}
