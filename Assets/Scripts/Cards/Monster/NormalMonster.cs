using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card.Monster
{
    public class NormalMonster : MonsterCard
    {
        public NormalMonster(string _cardName, int _attack, int _healthPointMax, params string[] _paras) : base(_cardName, _attack, _healthPointMax, _paras)
        {

        }

        public override string GetDesc()
        {
            return $"一条{name}";
        }
    }
}