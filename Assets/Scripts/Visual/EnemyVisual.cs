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

        #region  IPointerDownHandler实现区域
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Selections.Instance.CanSelect(this))
                Selections.Instance.AddSelection(this);
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
        public void UpdateSelectableVisual()
        {
            if (Selections.Instance.CanSelect(this))
                selectableEdge.SetActive(true);
            else
                selectableEdge.SetActive(false);
        }
        #endregion

        #region IUpdateVisual实现区域
        public void UpdateVisual()
        {
            if (descText) descText.text = enemy.GetDesc();
            if (healthText) healthText.text = enemy.hp + "/" + enemy.maxHp;
            if (nameText) nameText.text = enemy.name;
        }
        #endregion

    }

}
