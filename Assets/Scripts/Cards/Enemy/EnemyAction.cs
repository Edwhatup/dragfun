using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Card.Enemy {
    public class EnemyAction
    {
        public EnemyCard enemy;
        public int turn;
        public Action action;
        public EnemyActionType type;
        public int timer;
        public EnemyAction(EnemyCard enemy, int turn, Action action, EnemyActionType type)
        {
            this.enemy = enemy;
            this.turn = turn;
            this.action = action;
            this.type = type;
            ResetTimer();
        }
        public void ResetTimer()
        {
            timer = turn;
        }
    }
    public enum EnemyActionType
    {
        Once,
        Loop
    }
}
