using Card;
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

        


        #region ISeletableSource 实现区域

        public bool JudgeCanSelect(ISeletable seletable)
        {
            if(card is SpellCard)
            {
                if ((card as SpellCard).targetCount == 0) return seletable is SpellCastRegion;
                switch((card as SpellCard).cardTargets[Selections.Instance.NextSelectIndex])
                {
                    case CardTarget.Cell:
                        return seletable is Cell;
                    case CardTarget.Enemy:
                        return seletable is EnemyVisual;
                    case CardTarget.Monster:
                        return (seletable as PlayerCardVisual)?.card is MonsterCard;
                }
            }
            else if(card is MonsterCard)
            {
                switch(card.state)
                {
                    case PlayerCardState.InHand:
                        if (seletable is Cell && (seletable as Cell).cardVisual == null && (seletable as Cell).movable)
                            return true;                        
                        break;
                    case PlayerCardState.OnBoard:
                        switch (seletable.GetType().Name)
                        {
                            case nameof(EnemyVisual):
                                return (card as MonsterCard).CanAttack() && CellManager.Instance.GetCellRow(cell) < (card as MonsterCard).atkRange;
                            case nameof(Cell):
                                return CellManager.Instance.CellDistance((seletable as Cell), cell) == 1;
                            case nameof(PlayerCardVisual):
                                var visual=seletable as PlayerCardVisual;
                                return visual.card.state==PlayerCardState.OnBoard && CellManager.Instance.CellDistance(visual.cell,cell) == 1;
                        }
                        break;
                }
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

        public bool CanSelect()
        {
            return true;
        }
        public void UpdateSelectableVisual()
        {
            if (Selections.Instance.CanSelect(this))
                selectableEdge.color = Color.green;
            else
                selectableEdge.color = Color.white;
        }
        public int TargetCount
        {
            get
            {
                if (card is SpellCard) return (card as SpellCard).targetCount;
                else return 1;
            }
        }


        #endregion
        #region IPointerDownHandler 实现区域
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Selections.Instance.CanSelect(this))
            {
                if(Selections.Instance.HasSelectObject)
                    Selections.Instance.AddSelection(this);
                else Selections.Instance.AddSelectionSource(this);
            }
        }
        #endregion
        #region IUpdateVisual实现区域
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

        
        #endregion


    }
}