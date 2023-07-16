using UnityEngine;

public class StatsPositiveBuff : CardBuff
{
    private int atk = 0;
    private int hp = 0;
    private int extraDamage = 0;

    public StatsPositiveBuff(int atk, int hp) : base("身材加成", -1, BuffType.Positive, BuffLifeType.Board)
    {
        if (atk < 0) Debug.LogError("错误: 攻击加成<0!");
        this.atk = atk;
        this.hp = hp;
    }

    public StatsPositiveBuff(int atk, int hp, int extraDamage) : base("身材加成", -1, BuffType.Positive, BuffLifeType.Board)
    {
        if (atk < 0) Debug.LogError("错误: 攻击加成<0!");
        this.atk = atk;
        this.hp = hp;
        this.extraDamage = extraDamage;
    }

    public override void Execute()
    {
        if (card.attack != null)
        {
            card.attack.atk += atk;
            card.attacked.maxHp += hp;
            card.attacked.hp += hp;
            card.attack.extraDamage += extraDamage;
        }
    }

    public override void Undo()
    {
        if (card.attack != null)
            card.attack.atk -= atk;
        card.attacked.maxHp += hp;
        card.attacked.hp -= hp;
        card.attack.extraDamage -= extraDamage;


    }

    public override string GetDesc()
    {
        if (extraDamage != 0) return $"额外造成{extraDamage}点伤害, 剩余{lifeTimer}";
        else return $"获得+{atk}/+{hp}, 剩余{lifeTimer}";
    }
}
