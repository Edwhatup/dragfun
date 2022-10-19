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
            atkRange=1+RangeModifier;
        }
        public override string GetDesc()
        {
            return $"增程{RangeModifier}";
        }
    }
}
