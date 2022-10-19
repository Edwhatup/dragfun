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
        public bool movable=true;
        public PlayerCardVisual cardVisual = null;
        [SerializeField]
        GameObject SummonCell;
      
        public void SummonMonster(PlayerCardVisual cardVisual)
        {
            if (cardVisual.cell != null) cardVisual.cell.cardVisual = null;
            cardVisual.cell = this;
            this.cardVisual = cardVisual;
            cardVisual.transform.SetParent(this.transform);
            cardVisual.transform.localPosition = Vector3.zero;
        }
        #region IPointerDownHandler实现区域
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Selections.Instance.CanSelect(this))
                Selections.Instance.AddSelection(this);
        }
        #endregion
        #region ISeletable实现区域
        public void UpdateSelectableVisual()
        {
            if (Selections.Instance.CanSelect(this))
                SummonCell.SetActive(true);
            else SummonCell.SetActive(false);
        }

        public void Start()
        {
            Selections.Instance.AddCanSelection(this);
            if (cardVisual != null) SummonMonster(cardVisual);
        }
        public void OnDestroy()
        {
            Selections.Instance?.RemoveCanSelection(this);
        }
        #endregion

    }

}