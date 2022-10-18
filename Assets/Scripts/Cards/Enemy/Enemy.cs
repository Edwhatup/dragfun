using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Card.Enemy
{
    public abstract class NormalEnemy : EnemyCard
    {
        public NormalEnemy(string _cardName, int _healthPointMax, params string[] paras) : base(_cardName, _healthPointMax, paras)
        {

        }
    }

    public abstract class BossEnemy : EnemyCard
    {
        public BossEnemy(string _cardName, int _healthPointMax, params string[] paras) : base(_cardName, _healthPointMax, paras)
        {
        }
    }
}


