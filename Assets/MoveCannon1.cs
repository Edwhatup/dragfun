using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Core;

namespace Card.Monster
{


    public class MoveCannon1 : MonsterCard
    {
        int moveCount=0;
        int moveEffectCount;
        int effectDamage;

        public MoveCannon1(string name, int atk ,int hp,params string[] paras): base(name, atk, hp, paras)
        {
            int.TryParse(paras[0], out moveEffectCount);
            int.TryParse(paras[1], out effectDamage);
        }


        [EventListener]
        
        public void MoveCannon(object o)
        {
            MonsterMoveEvent moveEvent = o as MonsterMoveEvent;
            PlayerCard card = this as MonsterCard;
            if(moveEvent != null && moveEvent.IsSwap()==true && card.state==PlayerCardState.OnBoard)
            {
                //Debug.Log("Cannon Load!");
                moveCount+=2;
            }
            else if(moveEvent != null && moveEvent.IsSwap()==false && card.state==PlayerCardState.OnBoard)
            {
                moveCount+=1;
            }
            else moveCount+=0;

            if(moveCount >= moveEffectCount)
            {
                var randomEnemy = EnemyManager.Instance.GetRandomEnemy();
                Debug.Log("Cannon Out!");
                BattleManager.ApplyDamage(this,randomEnemy,effectDamage);
                moveCount = 0;
            }

        }

        public override string GetDesc()
        {
            return $"场上移动{moveEffectCount}(当前{moveCount}次)：对随机敌人造成{effectDamage}伤害";
        }


    }

}
