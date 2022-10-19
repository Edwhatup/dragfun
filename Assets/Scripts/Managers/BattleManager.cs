using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Card;
using Card.Monster;
using Card.Spell;
using Card.Enemy;
using Visual;
using Seletion;

namespace Core
{
    //逻辑立即执行，视觉表现添加到队列
    public static class BattleManager
    {
        public static event CardEventListen cardEventListeners;
        public static void CastSpell()
        {
            Debug.Log("CastSpell");
            var spell = Selections.Instance.SelectSource as SpellCard;
            spell.Cast();
            cardEventListeners?.Invoke(new UseSpellEvent(spell));
            CardManager.Instance.UseSpell(spell);

            Selections.Instance.Clear();
            GameManager.Instance.Refresh();
        }
        public static void SummonMonster(Cell cell)
        {
            var monster = Selections.Instance.SelectSource as MonsterCard;
            var visual = Selections.Instance.Selection as PlayerCardVisual;
            cell.SummonMonster(visual);
            CardManager.Instance.Summon(monster);
            //EffectQueue.AddEffect(new SunmonEffect(visual,cell));
            cardEventListeners?.Invoke(new SummonMonsterEvent(monster, cell));

            Selections.Instance.Clear();
            GameManager.Instance.Refresh();
        }
        public static void MonsterAttack()
        {
            PlayerCardVisual cardVisual = Selections.Instance.Selection as PlayerCardVisual;
            EnemyVisual enemyVisual = Selections.Instance.selections[1] as EnemyVisual;
            var monster = cardVisual.card as MonsterCard;
            var enemy = enemyVisual.enemy;
            AttackEvent attackEvent = new AttackEvent(monster, enemy);
            cardEventListeners?.Invoke(attackEvent);

            enemy.hp -= monster.atk;
            if (enemy.hp < 0)
            {
                enemy.battleState = BattleState.Dead;
                cardEventListeners?.Invoke(new DeathEvent(enemy, monster));
            }

            Selections.Instance.Clear();
            GameManager.Instance.Refresh();
        }

        public static void ApplyDamage(AbstractCard source, EnemyCard enemy, int damage)
        {
            enemy.hp -= damage;
            if (enemy.hp < 0)
            {
                enemy.battleState = BattleState.Dead;
                cardEventListeners?.Invoke(new DeathEvent(enemy, source));
            }
        }

        public static void Buff(AbstractCard source, MonsterCard monster, int atkModifier, int hpModifier)
        {
            monster.atk += atkModifier;
            monster.maxHp += hpModifier;
            monster.hp += hpModifier;
        }



        public static void MoveMonster(Cell cell)
        {
            Debug.Log("MoveMonster");
            var monster = Selections.Instance.SelectSource as MonsterCard;
            var visual = Selections.Instance.Selection as PlayerCardVisual;
            MonsterMoveEvent action = new MonsterMoveEvent(monster, visual.cell, cell);
            cell.SummonMonster(visual);
            cardEventListeners?.Invoke(action);

            Selections.Instance.Clear();
            GameManager.Instance.Refresh();
        }
    }
}