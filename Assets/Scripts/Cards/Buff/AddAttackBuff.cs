using UnityEngine;

public class AddAttackBuff : CardBuff
{
    private int amount = 0;
    public AddAttackBuff(int amount)
            : base("攻击加成", 3, BuffType.Positive, BuffLifeType.Board)
    {
        if(amount < 0 ) Debug.LogError("错误: 攻击加成<0!");
        this.amount = amount;
    }

    public override void Execute()
    {
        if (card.attack != null)
            card.attack.atk += amount;
    }

    public override void Undo()
    {
        if (card.attack != null)
            card.attack.atk -= amount;
    }

    public override string GetDesc() => $"攻击上升{amount}点, 剩余{lifeTimer}";
}
