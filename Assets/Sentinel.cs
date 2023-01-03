using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Core;

//没做完还
namespace Card.Monster
{
    
    public class Sentinel : MonsterCard
    {
        int dragCount;
        public Sentinel(string name, int atk ,int hp,params string[] paras): base(name, atk, hp, paras)
        {
            int.TryParse(paras[0], out dragCount);
        }

        [EventListener]
        public void OnAttack(object o)
        {
            
        }
        public override string GetDesc()
        {
            return $"每次攻击，抽{dragCount}张牌";
        }
    }
}