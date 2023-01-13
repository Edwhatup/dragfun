﻿using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对指定友方增加护甲
/// </summary>
public class AddArmour : CardEffect
{
    int armourValue;
    public AddArmour(string[] paras)
    {
        int.TryParse(paras[0], out armourValue);
        InitTarget();
    }
    public override bool CanUse()
    {
        return CardManager.Instance.board.FindAll(c=>c.camp==CardCamp.Friendly)!=null;
    }
    public AddArmour(int armourValue)
    {
        this.armourValue = armourValue;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets = new List<CardTarget>() {CardTarget.Monster};
    }
    public override void Excute()
    {
        var monster =(Targets[0] as CardVisual).card;
        monster.attacked.block+=armourValue;
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return $"使一名友方随从获得护甲值{armourValue}";
    }
}