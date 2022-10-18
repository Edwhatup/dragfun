using Card;
using Card.Enemy;
using Card.Monster;
using Card.Spell;
using Core;
using Seletion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Visual
{

    public class EnemyVisual : MonoBehaviour, IPointerDownHandler, IUpdateVisual, ISeletable
    {
        [SerializeField]
        Text nameText;
        [SerializeField]
        Text healthText;
        [SerializeField]
        Text descText;
        [SerializeField]
        GameObject selectableEdge;
        public EnemyCard enemy;

        public void Start()
        {
            Selections.Instance.AddCanSelection(this);
        }
        public void OnDestroy()
        {
            Selections.Instance?.RemoveCanSelection(this);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (CanSelect())
            {
                Selections.Instance.AddSelection(this);
                var card = Selections.Instance.SelectSource;
                if (card is SpellCard)
                    BattleManager.CastSpell();
                else BattleManager.MonsterAttack();
            }
        }

        public bool CanSelect()
        {
            var card = Selections.Instance.SelectSource;
            if (Selections.Instance.CurrentTarget == CardTarget.Enemy || (card is MonsterCard && card.state == PlayerCardState.OnBoard))
            {
                return true;
            }
            else return false;
        }

        public void UpdateSelectableVisual()
        {
            if (CanSelect())
                selectableEdge.SetActive(true);
            else
                selectableEdge.SetActive(false);
        }

        public void UpdateVisual()
        {
            if (descText) descText.text = enemy.GetDesc();
            if (healthText) healthText.text = enemy.hp + "/" + enemy.maxHp;
            if (nameText) nameText.text = enemy.name;
        }
    }

}
