using CardEvent;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card.Monster
{
    public class OnSwapBuffMonster : MonsterCard
    {

        int hpModifierOnAttack;
        int atkModiferOnAttack;

        public OnSwapBuffMonster(string name, int atk, int hp, params string[] paras) : base(name, atk, hp, paras)
        {

            int.TryParse(paras[0], out hpModifierOnAttack);
            int.TryParse(paras[1], out atkModiferOnAttack);
        }
        [EventListener]
        public void OnSwap(object o)
        {
            SwapEvent swapEvent = o as SwapEvent;
            if (swapEvent != null && swapEvent.sourceCard != this && swapEvent.sourceCard!=this)
            {
                BattleManager.Buff(this,this,hpModifierOnAttack,atkModiferOnAttack);
            }
        }
        public override string GetDesc()
        {
            return $"当其他随从发生交换时，获得+{hpModifierOnAttack}+{atkModiferOnAttack}";
        }

    }
}


