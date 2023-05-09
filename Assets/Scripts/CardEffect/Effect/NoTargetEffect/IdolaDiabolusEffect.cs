using System;
using System.Collections;
using System.Collections.Generic;

public class IdolaDiabolusEffect : NoTargetCardEffect
{
    int damage;
    RangeType rangeType;
    public IdolaDiabolusEffect(Card card, string[] paras) : base(card)
    {
        int.TryParse(paras[0], out damage);
        Enum.TryParse(paras[1],false,out RangeType rangeType);
    }
    public IdolaDiabolusEffect(Card card, int damage,RangeType rangeType) : base(card)
    {
        this.damage = damage;
        this.rangeType = rangeType;
    }

    public override string ToString()
    {
        return $"该随从死亡后也会保留本次战斗中所有获得的强化效果。";
    }

    public override void Excute()
    {
        //血量buff重置问题还没修完先挖个坑在这
    }
}
