using System;
using UnityEngine;
[CanRepeat(false)]
[RequireCardComponent(typeof(FieldComponnet))]
public class AttackedComponent : CardComponent
{
    public int hp;
    public int initMaxHp;
    public int maxHp;
    public int bless = 0;
    public int block = 0;
    public int taunt;
    public bool Taunt => taunt > 0;
    public bool Bless => bless > 0;

    /// <summary>
    /// 不可选中的状态码。如果要改变其不可选中状态，则应当增加这个值
    /// <para>作为int是为了防止多个buff同时修改时，有一个buff失效即取消其不可选中状态的情况</para>
    /// <para>应当调用<see cref="AttackedComponent.Selectable">来读取这个单位是否可以被选中</para>
    /// </summary>
    public int UnselectableState { private get => unselectableState; set => unselectableState = Mathf.Max(0, value); }
    private int unselectableState = 0;

    /// <summary>
    /// 这个单位是否可以被选中？
    /// </summary>
    public bool Selectable => UnselectableState == 0;

    public Card lastAttacker;

    public int GetAttackedPriority(Card card)
    {
        //敌人对我方单位的优先性计算
        if (this.card.camp == CardCamp.Friendly && card.camp == CardCamp.Enemy)
        {
            int res = 0;
            //我方单位有嘲讽
            if (Taunt) res += 1000;
            switch (card.type)
            {
                //敌人攻击最近的敌人，从上到下 从左到右
                case CardType.Enemy:
                    res += this.card.field.row.Value * 10 + this.card.field.col.Value;
                    break;
                //敌人衍生物攻击距离最近的敌人
                case CardType.EnemyDerive:
                    res += Math.Max(Math.Abs(card.field.row.Value - this.card.field.row.Value), Math.Abs(card.field.col.Value - this.card.field.col.Value));
                    break;

            }
            return res;
        }
        //我方单位对敌方的优先性计算
        else if (this.card.camp == CardCamp.Enemy && card.camp == CardCamp.Friendly)
        {
            //优先攻击有嘲讽的随从
            if (Taunt)
                return 1000;
            else return 1;
        }
        throw new Exception("错误的目标");

    }
    public AttackedComponent(int maxHp)
    {
        this.hp = maxHp;
        this.initMaxHp = maxHp;
        this.maxHp = maxHp;
    }

    public override void Recycle()
    {
        maxHp = initMaxHp;
        hp = maxHp;
    }

    public void Destroy(Card card)
    {
        card.field.state = BattleState.HalfDead;
        GameManager.Instance.Refresh();
        card.visual.UpdateVisual();
    }

    // public override void ResetnTurnStart()
    // {
    //     hp = maxHp;
    // }
    //public void Heal(Card source, int delta)
    //{
    //    var e=new HealEvent(source,card,delta);
    //    EventManager.Instance.PassEvent(e);
    //    this.hp.value += delta;
    //    if(Hp>MaxHp) hp.value = MaxHp;
    //    EventManager.Instance.PassEvent(e);
    //}
    public DamageInfo ApplyDamage(Card source, int damage, DamageType type = DamageType.Other)
    {
        int finalDamage = damage;
        if (type == DamageType.Attack)
            finalDamage = (int)((damage + source.attack.extraDamage) * (1 + source.attack.extraDamageRate));
        DamageInfo info = new DamageInfo()
        {
            initDamage = damage,
            beforeState = card.field.state,
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
            if (block > 0 && block >= finalDamage)
            {
                block -= finalDamage;
                info.actualDamage = 0;
                info.isResist = true;
            }
            else
            {
                finalDamage -= block;
                block = 0;
                info.actualDamage = finalDamage;
                info.isResist = false;
                this.hp -= finalDamage;
            }
        }
        if (this.hp <= 0)
        {
            card.field.state = BattleState.HalfDead;
            // Debug.Log($"{card.name} 进入濒死状态");
        }
        card.visual.UpdateVisual();
        var ae = new AfterDamageEvent(source, card, info);
        EventManager.Instance.PassEvent(ae);
        lastAttacker = source;

        if (card.field.state != BattleState.Survive) GameManager.Instance.Refresh();

        return info;
    }

    public int GetAttackDistance(Card card)
    {
        var res = MaxAbs(card.field.row - this.card.field.row, card.field.col - this.card.field.col);
        return res;

    }
    int MaxAbs(int? l, int? r)
    {
        if (l == null && r == null) throw new Exception();
        if (l == null) return Mathf.Abs(r.Value);
        if (r == null) return Mathf.Abs(l.Value);
        return Mathf.Max(Mathf.Abs(l.Value), Mathf.Abs(r.Value));
    }
    public override string ToString()
    {
        string str = this.hp.ToString();
        if (block != 0)
            str += $"({block})";
        if (Bless)
            str = '(' + str + ")";

        if (hp > maxHp) return $"<color=green>{str}</color>";
        if (hp == maxHp)
        {
            if (maxHp > initMaxHp)
                return $"<color=green>{str}</color>";
            else return $"<color=white>{str}</color>";
        }
        // Debug.Log($"name {card.name} ,hp {hp}, max {maxHp}, init {initMaxHp}");
        return $"<color=red>{str}</color>";
    }
}
