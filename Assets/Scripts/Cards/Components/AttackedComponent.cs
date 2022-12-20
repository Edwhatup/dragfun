using System;

[CanRepeat(false)]
public class AttackedComponent : CardComponent
{
    public int hp;
    public int initMaxHp;
    public int maxHp;
    public int bless=0;
    public int block = 0;
    public int taunt;
    public bool Taunt => taunt > 0;
    public bool Bless => bless > 0;
    public int GetAttackedPriority(Card card)
    {

        return 0;

    }
    public AttackedComponent(int maxHp)
    {
        this.hp = maxHp;
        this.initMaxHp = maxHp;
        this.maxHp = maxHp;
    }
    //public void Heal(Card source, int delta)
    //{
    //    var e=new HealEvent(source,card,delta);
    //    EventManager.Instance.PassEvent(e);
    //    this.hp.value += delta;
    //    if(Hp>MaxHp) hp.value = MaxHp;
    //    EventManager.Instance.PassEvent(e);
    //}
    public DamageInfo ApplyDamage(Card source, int damage)
    {
        DamageInfo info = new DamageInfo() 
        {
            initDamage = damage ,
            beforeState=card.field.state,
        };
        var e = new BeforeDamageEvent(source, card, damage);
        EventManager.Instance.PassEvent(e);
        if (Bless)
        {
            bless = 0;
            info.actualDamage = 0;
            info.isResist = true;
        }
        else
        {
            if (block > 0 && block >= damage)
            {
                block -= damage;
                info.actualDamage = 0;
                info.isResist = true;
            }
            else
            {
                damage -= block;
                block = 0;
                info.actualDamage = damage;
                info.isResist = true;
                this.hp -= damage;                
            }
        }
        if (this.hp <= 0)
        {
            card.field.state = BattleState.HalfDead;
        }
        var ae = new AfterDamageEvent(source, card, info);
        EventManager.Instance.PassEvent(ae);
        return info;
    }

    internal int GetAttackDistance(Card card)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        string str = this.hp.ToString();
        if (block == 0)
            str += $"({block})";
        if(Bless)
            str='('+str+")";
        if(this.hp==this.maxHp)
        {
            if (this.maxHp > this.initMaxHp)
                return $"<color=green>{str}</color>";
            else return $"<color=white>{str}</color>";
        }
        return $"<color=red>{str}</color>";
    }
}
