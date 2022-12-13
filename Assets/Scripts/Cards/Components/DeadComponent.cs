using System;
using System.Collections.Generic;
using System.Text;

public class DeadComponent : CardComponent
{
    List<CardEffect> deadEffects;
    public DeadComponent(CardEffect deadEffect)
    {
        this.deadEffects = new List<CardEffect>() { deadEffect };
    }
    public void Excute()
    {
        foreach (var dead in deadEffects)
            if (dead.CanUse()) dead.Excute();
    }

    public void AddDeadEffect(CardEffect deadEffect)
    {
        deadEffects.Add(deadEffect);
    }
    public override string Desc()
    {
        StringBuilder sb = new StringBuilder();
        if (deadEffects.Count > 0)
        {
            sb.Append("死亡时：");
            foreach (var deadEffect in deadEffects)
            {
                sb.Append(deadEffect.ToString());
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append('。');
        }
        return sb.ToString();
    }

    public override void Add(CardComponent component)
    {
        if (component is DeadComponent dead)
            deadEffects.Add(dead.deadEffects[0]);
    }
}