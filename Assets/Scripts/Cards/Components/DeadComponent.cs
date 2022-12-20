using System;
using System.Collections.Generic;
using System.Text;
[CanRepeat(true)]
public class DeadComponent : CardComponent
{
    CardEffect deadEffect;
    public DeadComponent(CardEffect deadEffect)
    {
        this.deadEffect = deadEffect ;
    }
    public void Excute()
    {
        if (deadEffect.CanUse()) deadEffect.Excute();
    }
    public override string ToString()
    {
        return $"死亡时：{deadEffect.ToString()}。";
    }
}