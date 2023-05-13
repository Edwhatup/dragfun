using UnityEngine;


public class CreatureReinforceBuff : CardBuff
{
    private int i= Random.Range(1,6);
    private CardBuff buff;

    public CreatureReinforceBuff() : base("造型强化", -1, BuffType.Positive, BuffLifeType.Board)
    {

    }

    public override void Execute()
    {

        switch (i)
            {
                case 1:
                    buff = new StatsPositiveBuff(3,3);
                    break;
                case 2:
                    buff =new RangeUpBuff(1);
                    break;
                case 3:
                    buff =new BlessBuff();
                    break;
                case 4:
                    buff =new SwiftBuff();
                    break;
                case 5:
                    buff =new TauntBuff();
                    break;           
            }
        card.AddSubBuff(buff);
    }

    public override void Undo()
    {
        card.RemoveSubBuff(buff);
    }

    public override string GetDesc() => $"获得一次造型强化, 剩余{lifeTimer}";
}
