﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Core;

namespace Card.Monster 
{
    public class OnAttack1 : MonsterCard
    {
        int hpModifierOnAttack;
        int atkModifierOnAttack;
        public OnAttack1(string name, int atk, int hp, params string[] paras) : base(name, atk, hp, paras)
        {
            int.TryParse(paras[0], out hpModifierOnAttack);
            int.TryParse(paras[1], out atkModifierOnAttack);
            targetCount = 1;
            cardTargets = new CardTarget[1] { CardTarget.Enemy };
        }
        [EventListener]
        public void OnAttack(object o)
        {
            AttackEvent attackEvent = o as AttackEvent;
            if (attackEvent != null && attackEvent.source == this)
            {
                BattleManager.Buff(this, this, hpModifierOnAttack, atkModifierOnAttack);
                GameManager.Instance.Refresh();
            }
        }
        public override string GetDesc()
        {
            return $"攻击时，获得+{hpModifierOnAttack}/+{atkModifierOnAttack}";
        }
    }
}