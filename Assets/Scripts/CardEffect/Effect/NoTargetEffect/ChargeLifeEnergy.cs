using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeLifeEnergy : NoTargetCardEffect
{
    int points;

    public ChargeLifeEnergy(Card card, string[] paras) : base(card)
    {
        int.TryParse(paras[0], out points);
    }
    public ChargeLifeEnergy(Card card, int points) : base(card)
    {
        this.points = points;
    }

    public override string ToString()
    {
        return $"";
    }

    public override void Excute()
    {
        GameManager.Instance.LifeEnergyPoint += points;

    }
}
