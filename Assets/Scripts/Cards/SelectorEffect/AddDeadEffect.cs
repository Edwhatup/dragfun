﻿using System.Collections.Generic;
using System.Linq;

//为一名友方随从添加死亡效果
public class AddDeadEffect : CardEffect
{
    CardEffect deadEffect;
    public AddDeadEffect(string[] paras):base()
    {
        deadEffect = CardEffect.GetCardEffect(paras[0], paras.Skip(1).ToArray());
    }

    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets = new List<CardTarget>() { CardTarget.Monster };
    }
    public AddDeadEffect(CardEffect effect) : base()
    {
        deadEffect = effect;
    }
    public override void Excute()
    {
        var monster = (Targets[0] as CardVisual).card;
        monster.AddComponnet(new DeadComponent(deadEffect));
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return $"为一个随从添加死亡效果：{deadEffect.ToString()}";
    }
}