using UnityEngine;

public class CreatureReinforceBuff : CardBuff
{

    public CreatureReinforceBuff() : base("造型强化", -1, BuffType.Positive, BuffLifeType.Board)
    {

    }

    public override void Execute()
    {
        int i= Random.Range(1,6);
            switch (i)
            {
                case 1:
                    card.AddBuff(new StatsPositiveBuff(3,3));
                    break;
                case 2:
                    card.AddBuff(new RangeUpBuff(1));
                    break;
                case 3:
                    card.AddBuff(new BlessBuff());
                    break;
                case 4:
                    card.AddBuff(new SwiftBuff());
                    break;
                case 5:
                    card.AddBuff(new TauntBuff());
                    break;           
            }
    }

    public override void Undo()
    {
        if (card.attacked != null)
            card.attacked.bless =0;
        //undo效果没做完，这玩意封装疑似不好做，考虑buff参数保留方便undo,或者嵌套buff做一个新的基类


    }

    public override string GetDesc() => $"获得一次造型强化, 剩余{lifeTimer}";
}
