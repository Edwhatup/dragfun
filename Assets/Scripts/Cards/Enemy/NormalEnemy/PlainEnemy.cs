using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card.Enemy
{
    public class PlainEnemy : NormalEnemy
    {
        public PlainEnemy(string _cardName, int _healthPointMax, params string[] paras) : base(_cardName, _healthPointMax, paras)
        {
            actions.Add(new EnemyAction(this, 1,
                    () => {
                        EnemyActions.ApplyDamage2Player(this, 2);
                    },
                    EnemyActionType.Loop));
        }
        public override string GetDesc()
        {
            return "每过1个回合，就对敌人造成2点伤害";
        }
    }
}