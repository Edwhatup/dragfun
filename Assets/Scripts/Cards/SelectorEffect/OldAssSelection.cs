using System;
using System.Collections;
using System.Collections.Generic;

public class OldAssSelection : CardEffect
{
    int hpModifier;
    int atkMofifier;
    public OldAssSelection(string[] paras) : base()
    {
        //没写完，摸了
    }
    public OldAssSelection(int hpModifier, int atkMofifier) : base()
    {
        //没写完，摸了
    }

    public override string ToString()
    {
        return $"如果在第一行，则永久+3/+3，如果在其他位置，则对随机敌人主体造成等同于攻击力的伤害，获得嘲讽";
    }

    public override void Excute()
    {
        if(card.field.row==0)
        {
            var e=new StrengthMonsterEffect(hpModifier,atkMofifier);
            //这个地方效果不对，应该是增加永久身材效果
            card.AddComponnet(e);
        }
        if(card.field.row>=0)
        {
            //获得嘲讽
        }
            
    }

    public override void InitTarget()
    {
        NoTarget();
    }

    public override void OnSelected()
    {
        return;
    }
}
