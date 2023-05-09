using UnityEngine;

public class BlessBuff : CardBuff
{

    public BlessBuff() : base("护佑", -1, BuffType.Positive, BuffLifeType.Board)
    {

    }

    public override void Execute()
    {
        if (card.attacked != null)
            card.attacked.bless =1;
    }

    public override void Undo()
    {
        if (card.attacked != null)
            card.attacked.bless =0;


    }

    public override string GetDesc() => $"获得护佑, 剩余{lifeTimer}";
}
