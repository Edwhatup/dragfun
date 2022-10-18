using Core;
using Seletion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace Card.Spell
{
    public class SingleDamage : SpellCard
    {
        public int damage;
        public SingleDamage(string name, params string[] paras) : base(name, paras)
        {
            ParseHelper.CheckParamsCountWithMessage(paras, 1, nameof(SingleDamage), name);
            damage = ParseHelper.ParseCardIntWithMessage(paras[0], nameof(SingleDamage), name);
            this.targetCount = 1;
            this.cardTargets = new CardTarget[1] { CardTarget.Enemy };
        }
        public override void Cast()
        {
            var enemy = (Selections.Instance.selections[1] as EnemyVisual).enemy;
            BattleManager.ApplyDamage(this, enemy, damage);
        }

        public override string GetDesc()
        {
            return $"对一名敌人造成{damage}点伤害";
        }
    }
}