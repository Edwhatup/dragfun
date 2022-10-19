using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Core;

namespace Card.Monster
{
    public class OnMove1 : MonsterCard
    {
        int hpModifierOnMove;
        int atkModifierOnMove;

        public OnMove1(string name, int atk ,int hp,params string[] paras): base(name, atk, hp, paras)
        {
            int.TryParse(paras[0], out hpModifierOnMove);
            int.TryParse(paras[1], out atkModifierOnMove);
            targetCount = 1;
            cardTargets = new CardTarget[1] { CardTarget.Enemy };
        }

        [EventListener]
        public void OnMove(object o)
        {
            MonsterMoveEvent moveEvent = o as MonsterMoveEvent;
            if(moveEvent != null)
            {
                BattleManager.Buff(this, this, hpModifierOnMove, atkModifierOnMove);
                GameManager.Instance.Refresh();
            }
        }
        public override string GetDesc()
        {
            return $"每次移动时，获得+{hpModifierOnMove}/+{atkModifierOnMove}";
        }

    }
}

