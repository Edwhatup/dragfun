using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Core;

namespace Card.Monster
{
    
    public class OnForward1 : MonsterCard
    {
        int hpModifierOnForward;
        int atkModifierOnForward;

        public OnForward1(string name, int atk ,int hp,params string[] paras): base(name, atk, hp, paras)
        {
            int.TryParse(paras[0], out hpModifierOnForward);
            int.TryParse(paras[1], out atkModifierOnForward);
        }

        [EventListener]
        public void OnForward(object o)
        {
            MonsterMoveEvent moveEvent = o as MonsterMoveEvent;
            PlayerCard card = this as MonsterCard;
            if(moveEvent != null && moveEvent.IsForward(this)==true && card.state==PlayerCardState.OnBoard)
            {
                Debug.Log("OnForward");
                BattleManager.Buff(this, this, hpModifierOnForward, atkModifierOnForward);
            }
        }
        public override string GetDesc()
        {
            return $"每次前进，获得+{hpModifierOnForward}/+{atkModifierOnForward}";
        }
    }
}
