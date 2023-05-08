using UnityEngine;

public class RangeUpBuff : CardBuff
{
    private int modValue;
    public RangeUpBuff(int modValue) : base("增程", -1, BuffType.Positive, BuffLifeType.Board)
    {
        if(modValue < 0 ) Debug.LogError("错误: modValue<0!");
        this.modValue=modValue;
    }

    public override void Execute()
    {
        if (card.attack != null)
            card.attack.atkRange += modValue;
    }

    public override void Undo()
    {
        if (card.attack != null)
            card.attack.atkRange -= modValue;


    }

    public override string GetDesc() => $"获得增程{modValue}, 剩余{lifeTimer}";
}
