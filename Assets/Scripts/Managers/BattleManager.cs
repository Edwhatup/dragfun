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
            var spell = Selections.Instance.SourceCard as SpellCard;
            CardManager.Instance.UseSpell(spell);

            spell.Cast();
            cardEventListeners?.Invoke(new UseSpellEvent(spell));
        }
        public static void SummonMonster()
        {
            var visual = Selections.Instance.Selection as PlayerCardVisual;
            var monster = Selections.Instance.SourceCard as MonsterCard;
            var cell = Selections.Instance.selections[1] as Cell;
            cell.SummonMonster(visual);
            CardManager.Instance.Summon(monster);

            cardEventListeners?.Invoke(new SummonMonsterEvent(monster, cell));
        }
        public static void MonsterAttack()
        {
            PlayerCardVisual cardVisual = Selections.Instance.Selection as PlayerCardVisual;
            EnemyVisual enemyVisual = Selections.Instance.selections[1] as EnemyVisual;
            var monster = cardVisual.card as MonsterCard;
            var enemy = enemyVisual.enemy;

            AttackEvent attackEvent = new AttackEvent(monster, enemy);
            cardEventListeners?.Invoke(attackEvent);

            ApplyDamage(monster, enemy, monster.atk);
        }
        public static void SwapMonster()
        {
            var visual1 = Selections.Instance.selections[0] as PlayerCardVisual;
            var visual2 = Selections.Instance.selections[1] as PlayerCardVisual;
            var cell2 = visual2.cell;
            visual1.cell.SummonMonster(visual2);
            cell2.SummonMonster(visual1);

            MonsterMoveEvent swapEvent = new MonsterMoveEvent(visual1.card as MonsterCard, visual2.card as MonsterCard, visual2.cell, visual1.cell);
            cardEventListeners?.Invoke(swapEvent);
        }
        public static void MoveMonster()
        {
            var monster = Selections.Instance.SourceCard as MonsterCard;
            var visual = Selections.Instance.Selection as PlayerCardVisual;
            var cell = Selections.Instance.selections[1] as Cell;
            cell.SummonMonster(visual);
            
            MonsterMoveEvent action = new MonsterMoveEvent(monster, visual.cell, cell);
            cardEventListeners?.Invoke(action);
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
    }
}