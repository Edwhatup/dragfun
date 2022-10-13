using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Card
{
    public string name;
    public string[] paras;
    string descriptions;
    public Card(string _cardName,params string[] _paras)
    {
        this.name = _cardName;
        this.paras = _paras;
    }
    public virtual string GetDesc()
    {
        return "";
    }
}
public abstract class BattleCard : Card
{
    public int healthPoint;
    public int healthPointMax;
    public int initHealthPointMax;
    public BattleState state;
    public BattleCard(string _cardName, int _healthPointMax,params string[] paras) : base(_cardName,paras)
    {
        this.healthPoint = _healthPointMax;
        this.healthPointMax = _healthPointMax;
        this.state = BattleState.Survive;
    }
    public virtual void ApplyDamage(Card source,int damage)
    {
        this.healthPoint -= damage;
        if(this.healthPoint<0)
        {
            this.state=BattleState.HalfDead;
            Dead();
        }   
    }
    public virtual void Buff(Card source, int hpModifier,int atkModifier=0)
    {
        this.healthPoint += hpModifier;
        this.healthPointMax += hpModifier;
    }
    public virtual void Dead()
    {

    }

    public enum BattleState
    {
        Survive,
        HalfDead,
        Dead
    }
}

public abstract class MonsterCard : BattleCard
{
    public int atk;
    public int atkRange;

    public MonsterCard(string _cardName,int _atk, int _healthPointMax, params string[] paras) : base(_cardName, _healthPointMax, paras)
    {
        this.atk = _atk;
        this.atkRange = 1;
    }
    public override void Buff(Card source, int hpModifier, int atkModifier = 0)
    {
        base.Buff(source, hpModifier, atkModifier);
        this.atk += atkModifier;
    }
}
public abstract class SpellCard : Card
{
    public int targetConut;
    public int targetType;

    public SpellCard(string _cardName,params string[] _paras) : base(_cardName, _paras) { }
    public abstract void Cast();

    public enum SpellCardTargetType
    {
        EnemyMonster,
        FriendlyMonster
    }
}