using Card.Enemy;
using Card.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CardEvent
{
    //游戏中的攻击事件，在攻击完成前广播
    public class AttackEvent
    {
        public MonsterCard source;
        public EnemyCard target;

        public AttackEvent(MonsterCard source, EnemyCard target)
        {
            this.source = source;
            this.target = target;
        }
    }
}