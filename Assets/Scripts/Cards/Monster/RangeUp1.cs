using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Core;

namespace Card.Monster 
{
    public class RangeUp1 : MonsterCard
    {
        int RangeModifier;
        public RangeUp1(string name, int atk, int hp, params string[] paras) : base(name, atk, hp, paras)
        {
            int.TryParse(paras[0], out RangeModifier);
            targetCount = 1;
            atkRange=1+RangeModifier;
            cardTargets = new CardTarget[1] { CardTarget.Enemy };
        }
        public override string GetDesc()
        {
            return $"增程{RangeModifier}";
        }
    }
}
