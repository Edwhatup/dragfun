using UnityEngine;

public class StatsDebuff : CardBuff
{
    private int atk = 0;
    private int hp = 0;
    private int extraDamage=0;

    public StatsDebuff(int atk,int hp) : base("身材减益", -1, BuffType.Negative, BuffLifeType.Board)
    {
        if(atk < 0 ) Debug.LogError("错误: 攻击加成<0!");
        this.atk = atk;
        this.hp=hp;
    }

        public StatsDebuff(int atk,int hp,int extraDamage) : base("削弱攻击", -1, BuffType.Negative, BuffLifeType.Board)
    {
        if(atk < 0 ) Debug.LogError("错误: 攻击加成<0!");
        this.atk = atk;
        this.hp=hp;
        this.extraDamage=extraDamage;
    }

    public override void Execute()
    {

        if (card.attack != null)
        {
            card.attack.atk -= atk;
            card.attack.extraDamage-=extraDamage;
        }
        card.attacked.hp-=hp;



    }

    public override void Undo()
    {

        if (card.attack != null)
        {
            card.attack.atk += atk;
            card.attack.extraDamage+=extraDamage;
        }
        card.attacked.hp+=hp;

    }

    public override string GetDesc() 
    {

        if(extraDamage==0) return $"-{atk}/-{hp}, 剩余{lifeTimer}";
        else if(atk == 0 && hp==0) return $"造成的伤害减少{extraDamage}, 剩余{lifeTimer}";
        else return $"-{atk}/-{hp},且造成的伤害减少{extraDamage}, 剩余{lifeTimer}";
    }
}
