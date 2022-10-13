using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : BattleCard
{
    public List<EnemyAction> actions;
    public Enemy(string _cardName, int _healthPointMax, params string[] paras) : base(_cardName, _healthPointMax, paras)
    {
        actions = new List<EnemyAction>();
    }
}
public abstract class NormalEnemy : Enemy
{
    public NormalEnemy(string _cardName, int _healthPointMax, params string[] paras) : base(_cardName, _healthPointMax, paras)
    {

    }
}

public abstract class BossEnemy : Enemy
{
    public BossEnemy(string _cardName, int _healthPointMax, params string[] paras) : base(_cardName, _healthPointMax, paras)
    {
    }
}
