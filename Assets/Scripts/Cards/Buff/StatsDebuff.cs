using UnityEngine;

public class StatsDebuff : CardBuff
{
    private int atk = 0;
    private int hp = 0;

    public StatsDebuff(int atk,int hp) : base("身材减益", 3, BuffType.Negative, BuffLifeType.Board)
    {
        if(atk < 0 ) Debug.LogError("错误: 攻击加成<0!");
        this.atk = atk;
        this.hp=hp;
    }

    public override void Execute()
    {
        if (card.attack != null)
            card.attack.atk -= atk;
            card.attacked.hp-=hp;
    }

    public override void Undo()
    {
        if (card.attack != null)
            card.attack.atk += atk;
            card.attacked.hp+=hp;


    }

    public override string GetDesc() => $"-{atk}/-{hp}, 剩余{lifeTimer}";
}
