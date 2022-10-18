using Card;
using Card.Monster;
using Card.Spell;
using Seletion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Visual
{
    public class PlayerCardVisual : MonoBehaviour, IUpdateVisual, ISeletableSource, IPointerDownHandler
    {
        public Text nameText;
        public Text attackText;
        public Text healthText;
        public Text effectText;
        public Image backgroundImage;
        public PlayerCard card;
        public Cell cell;
        [SerializeField]
        Image selectableEdge;
        public int TargetCount
        {
            get
            {
                if (card is SpellCard)
                    return card.targetCount;
                else
                {
                    return 1;
                    //switch (card.state)
                    //{
                    //    case PlayerCardState.OnBoard:
                    //        return card.targetCount;
                    //    case PlayerCardState.InHand:
                    //        return 1;
                    //    default: return 0;
                    //}
                }
            }
        }

        public CardTarget CurrentTarget
        {
            get
            {
                if (card is MonsterCard)
                {
                    switch (card.state)
                    {
                        case PlayerCardState.OnBoard:
                            return CardTarget.Enemy;
                        case PlayerCardState.InHand:
                            return CardTarget.Enemy;
                        default: return CardTarget.None;
                    }
                }
                else return card.cardTargets[Selections.Instance.CurrentSelectIndex];
            }
        }

        public bool CanSelect()
        {
            if (card.state == PlayerCardState.InHand)
            {
                if (!Selections.Instance.HasSelectObject)
                    return true;
                else return false;
            }
            else if (card.state == PlayerCardState.OnBoard)
            {
                if (!Selections.Instance.HasSelectObject || Selections.Instance.CurrentTarget == CardTarget.FriendlyMonster)
                    return true;
                else return false;
            }
            return false;
        }

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
            }
        }

        public void UpdateSelectableVisual()
        {
            if (CanSelect())
                selectableEdge.color = Color.green;
            else
                selectableEdge.color = Color.white;
        }
        public void UpdateVisual()
        {
            if (nameText) nameText.text = card.name;
            if (effectText) effectText.text = card.GetDesc();
            if (card is MonsterCard)
            {
                var monster = card as MonsterCard;
                if (attackText)
                {
                    attackText.gameObject.SetActive(true);
                    attackText.text = monster.atk.ToString();
                }
                if (healthText)
                {
                    healthText.gameObject.SetActive(true);
                    healthText.text = monster.hp + "/" + monster.maxHp;
                }
            }
            else if (card is SpellCard)
            {
                attackText.gameObject.SetActive(false);
                healthText.gameObject.SetActive(false);
            }
        }
    }
}