using Card;
using Card.Spell;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace Seletion
{
    public class Selections : MonoSingleton<Selections>
    {
        [SerializeField]
        GameObject arrowPrefab;
        [SerializeField]
        Transform arrowParentTrans;
        GameObject arrow;
        GameObject mouseFollowObject;

        public List<ISeletable> selections = new List<ISeletable>();
        List<ISeletable> allCanSelections = new List<ISeletable>();
        public bool HasSelectObject => selections.Count > 0;
        public ISeletableSource Selection => HasSelectObject ? selections[0] as ISeletableSource : null;
        public PlayerCard SourceCard => HasSelectObject ? (selections[0] as PlayerCardVisual)?.card : null;
        public int NextSelectIndex => selections.Count - 1;
        public bool HasFinishSelect => Selection != null && NextSelectIndex >= Selection.TargetCount;


        #region ISelectable 注册相关方法
        public void AddCanSelection(ISeletable seletable)
        {
            if (!allCanSelections.Contains(seletable))
            {
                allCanSelections.Add(seletable);
                seletable.UpdateSelectableVisual();
            }
        }
        public void RemoveCanSelection(ISeletable seletable)
        {
            if (allCanSelections.Contains(seletable))
                allCanSelections.Remove(seletable);
        }
        #endregion

        void Update()
        {
            if (mouseFollowObject)
            {
                Vector2 vector2 = Input.mousePosition;
                mouseFollowObject.transform.position = vector2;
            }
            if (Input.GetMouseButton(1))
            {
                Clear();
            }
        }
        public void Clear()
        {
            DestoryArrow();
            mouseFollowObject = null;
            selections.Clear();
            UpdateAllSelectableVisual();
        }
        public void AddSelectionSource(ISeletableSource source)
        {
            selections.Add(source);
            if (source.TargetCount == 1)
            {
                var startTrans = (source as MonoBehaviour).transform;
                CreateArrow(startTrans);                
            }
            else if (source.TargetCount == 0)
                mouseFollowObject = (source as MonoBehaviour).gameObject;
            UpdateAllSelectableVisual();
        }

        public void AddSelection(ISeletable item)
        {
            selections.Add(item);
            if(HasFinishSelect)
            {
                if (SourceCard is SpellCard)
                    BattleManager.CastSpell();
                else
                {
                    switch (item.GetType().Name)
                    {
                        case nameof(PlayerCardVisual):
                            BattleManager.SwapMonster();
                            break;
                        case nameof(Cell):
                            if (SourceCard.state == PlayerCardState.OnBoard)
                                BattleManager.MoveMonster();
                            else
                                BattleManager.SummonMonster();
                            break;
                        case nameof(EnemyVisual):
                            BattleManager.MonsterAttack();
                            break;
                    }
                }
                Clear(); 
                GameManager.Instance.Refresh();
            }

            UpdateAllSelectableVisual();
        }
        public bool CanSelect(ISeletable seletable)
        {
            if (!HasSelectObject && ((seletable as ISeletableSource)?.CanSelect()??false)) return true;
            return HasSelectObject && Selection.JudgeCanSelect(seletable);
        }

        private void CreateArrow(Transform startTrans)
        {
            arrow = GameObject.Instantiate(arrowPrefab, startTrans.position, Quaternion.identity, arrowParentTrans);
        }
        private void DestoryArrow()
        {
            Destroy(arrow);
        }
        public void UpdateAllSelectableVisual()
        {
            foreach (var selection in allCanSelections)
            {
                selection.UpdateSelectableVisual();
            }
        }
    }

}
