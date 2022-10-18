using Card;
using Card.Monster;
using Core;
using Seletion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Visual
{
    public class Cell : MonoBehaviour, IPointerDownHandler, ISeletable
    {
        public bool movable;
        public PlayerCardVisual cardVisual = null;
        [SerializeField]
        GameObject SummonCell;

        public void Start()
        {
            Selections.Instance.AddCanSelection(this);
            if (cardVisual != null) SummonMonster(cardVisual);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (CanSelect())
            {
                var monsterCard = Selections.Instance.SelectSource as MonsterCard;
                switch (monsterCard.state)
                {
                    case PlayerCardState.InHand:
                        BattleManager.SummonMonster(this);
                        break;
                    case PlayerCardState.OnBoard:
                        BattleManager.MoveMonster(this);
                        break;
                }
            }
        }
        public void SummonMonster(PlayerCardVisual cardVisual)
        {
            if (cardVisual.cell != null) cardVisual.cell.cardVisual = null;
            cardVisual.cell = this;
            this.cardVisual = cardVisual;
            cardVisual.transform.SetParent(this.transform);
            cardVisual.transform.localPosition = Vector3.zero;
        }
        public bool CanSelect()
        {
            var sourceCard = Selections.Instance.SelectSource;
            if (sourceCard is MonsterCard)
            {
                if (sourceCard.state == PlayerCardState.InHand && cardVisual == null)
                    return true;
                else if (sourceCard.state == PlayerCardState.OnBoard)
                {
                    var cell = (Selections.Instance.Selection as PlayerCardVisual).cell;
                    if (cell == this) return false;
                    if (CellManager.Instance.CellDistance(this, cell) <= 1)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            return false;
        }


        public void UpdateSelectableVisual()
        {
            if (CanSelect())
                SummonCell.SetActive(true);
            else SummonCell.SetActive(false);
        }


        public void OnDestroy()
        {
            Selections.Instance?.RemoveCanSelection(this);
        }
    }

}