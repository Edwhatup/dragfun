using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class Cell : MonoBehaviour, ISeletableTarget, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Card card;
    [SerializeField]
    GameObject selectableEdge;
    public int row;
    public int col;
    public Card preShowCard;
    bool mouseEnter = false;
    void Update()
    {
        if (mouseEnter)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                //Selections.Instance.TryAddSelectTarget(this);
            }
        }
    }
    public void PlaceCard(Card card)
    {
        var field = card.field;
        if (field != null)
        {
            if (field.cell != null) field.cell.card = null;
            field.cell = this;
            this.card = card;
            card.field.row = row;
            card.field.col = col;
            card.visual.transform.SetParent(transform, false);
            card.visual.transform.localPosition = Vector3.zero;
        }
    }
    public void CancleSummon()
    {
        if (card != null)
        {
            card.field.cell = null;

        }
    }
    public bool CanMove()
    {
        return card == null;
    }
    public bool CanSwaped()
    {
        if (card == null) return true;
        return card.field.CanSwap;
    }
    public bool CanSummon()
    {
        return card == null && preShowCard == null;
    }
    public bool CanCastSpell()
    {
        return card == null;
    }
    #region ISeletable实现区域

    public void Start()
    {
        Selections.Instance.AddCanSelection(this);
    }
    public void OnDestroy()
    {
        Selections.Instance?.RemoveCanSelection(this);
    }

    public void UpdateSelectableVisual(bool canSelect)
    {
        selectableEdge.SetActive(canSelect);
    }

    public void PreShowCard(Card card)
    {
        preShowCard = card;
        card.visual.transform.SetParent(transform, false);
        card.visual.transform.localPosition = Vector3.zero;
    }
    public void RemoveCard()
    {
        this.card = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseEnter = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -1 && eventData.used == false)
        {
            Selections.Instance.TryAddSelectTarget(this as ISeletableTarget);
        }
    }

    #endregion

}
