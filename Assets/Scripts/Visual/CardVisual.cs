using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public abstract class CardVisual : MonoBehaviour, IPointerDownHandler
{
    public Card card;
    [SerializeField]
    Image rayTarget;
    public virtual void Start()
    {
        Selections.Instance.AddCanSelection(this as ISeletableTarget);
        UpdateVisual();
    }
    public virtual void OnDestory()
    {
        Selections.Instance.RemoveCanSelection(this as ISeletableTarget);
    }
    public abstract void UpdateVisual();
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -1 && eventData.used==false)
        {
            bool asSelector = false;
            if (CardManager.Instance.hand.Contains(card))
                asSelector = Selections.Instance.TryAddSelector(card.use);
            else if (CardManager.Instance.board.Contains(card))
                asSelector = Selections.Instance.TryAddSelector(card.action);
            if (!asSelector)
                Selections.Instance.TryAddSelectTarget(this as ISeletableTarget);
            eventData.Use();
        }
    }

    public void SetCard(Card card)
    {
        this.card = card;
        card.visual = this;
    }

    public void SetRayCastTarget(bool v)
    {
        rayTarget.raycastTarget = v;
    }
}