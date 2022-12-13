using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class Cell : MonoBehaviour, IPointerDownHandler, ISeletableTarget
{
    public Card card;
    [SerializeField]
    GameObject selectableEdge;
    public int row;
    public int col;
    public void Summon(Card card)
    {
        var field = card.field;
        if(field != null)
        {
            if(field.cell!=null) field.cell.card = null;
            field.cell = this;
            this.card = card;
            card.visual.transform.SetParent(transform,false);
            card.visual.transform.localPosition= Vector3.zero;
        }
    }
    public void CancleSummon()
    {
        if(card!=null)
        {
            card.field.cell = null;

        }
    }
    public bool CanMove()
    {
        return true;
    }
    public bool CanSwaped()
    {

        return false;
    }

    #region IPointerDownHandler实现区域
    public void OnPointerDown(PointerEventData eventData)
    {
        Selections.Instance.TryAddSelectTarget(this);
    }
    public bool CanSummon()
    {
        return card == null;
    }
    public bool CanCastSpell()
    {
        return card == null;
    }
    #endregion
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
        card.state = CardState.PreUse;
        card.visual.transform.SetParent(transform,false);
        card.visual.transform.localPosition = Vector3.zero;
    }
    public void RemoveCard()
    {
        this.card = null;
    }
    #endregion

}
