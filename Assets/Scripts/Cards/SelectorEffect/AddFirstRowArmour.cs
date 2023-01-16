using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFirstRowArmour : CardEffect
{
     int armourValue;
    public AddFirstRowArmour(string[] paras)
    {
        int.TryParse(paras[0], out armourValue);
        InitTarget();
    }
    public override bool CanUse()
    {
        return true;
    }
    public AddFirstRowArmour(int armourValue)
    {
        this.armourValue = armourValue;
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

