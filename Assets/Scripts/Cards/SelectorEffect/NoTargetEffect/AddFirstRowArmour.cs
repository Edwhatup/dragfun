using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFirstRowArmour : NoTargetCardEffect
{
    int armourValue;
    public AddFirstRowArmour(Card card, string[] paras):base(card)
    {
        int.TryParse(paras[0], out armourValue);
        InitTarget();
    }
    public override bool CanUse()
    {
        return true;
    }
    public AddFirstRowArmour(Card card, int armourValue):base(card)
    {
        this.armourValue = armourValue;
    }

    public override void OnSelected()
    {
        return;
    }
    public override void Excute()
    {
        List<Card> Monsters =  CardManager.Instance.GetXRowMonsterUnits(1);
        foreach(var monster in Monsters)
        {
            monster.attacked.block+=armourValue;
        }
    }

    public override string ToString()
    {
        return $"使第一排的随从都获得护甲值{armourValue}";
    }
}

