using Card;
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
        Transform canvasTrans;
        GameObject arrow;
        GameObject arrowParent;
        public List<ISeletable> selections = new List<ISeletable>();
        List<ISeletable> allCanSelections = new List<ISeletable>();
        public bool HasSelectObject => selections.Count > 0;
        public ISeletableSource Selection => selections.Count > 0 ? selections[0] as ISeletableSource : null;
        public void AddCanSelection(ISeletable seletable)
        {
            if (!allCanSelections.Contains(seletable))
            {
                allCanSelections.Add(seletable);
            }
        }

        public PlayerCard SelectSource => selections.Count > 0 ? (selections[0] as PlayerCardVisual).card : null;
        public int CurrentSelectIndex => selections.Count - 1;
        public int TargetCount => Selection?.TargetCount ?? 0;
        public CardTarget CurrentTarget => TargetCount + 1 == selections.Count ? CardTarget.None : Selection?.CurrentTarget ?? CardTarget.None;

        public void RemoveCanSelection(ISeletable seletable)
        {
            if (allCanSelections.Contains(seletable))
                allCanSelections.Remove(seletable);
        }
        void Update()
        {
            if (arrowParent)
            {
                Vector2 vector2 = Input.mousePosition;
                arrowParent.transform.position = vector2;
            }
            if (Input.GetMouseButton(1))
            {
                Clear();
            }
        }
        public void Clear()
        {
            DestoryArrow();
            arrowParent = null;
            selections.Clear();
            UpdateAllSelectableVisual();
            GameManager.Instance.Refresh();
        }
        public void AddSelection(ISeletable item)
        {
            selections.Add(item);
            if (selections.Count == 1)
            {
                if (TargetCount == 1)
                {
                    var startTrans = (selections[0] as MonoBehaviour).transform;
                    CreateArrow(startTrans);
                }
                else if (TargetCount == 0)
                {
                    arrowParent = (Selection as MonoBehaviour).gameObject;
                }
            }
            UpdateAllSelectableVisual();
        }
        private void CreateArrow(Transform startTrans)
        {
            arrow = GameObject.Instantiate(arrowPrefab, startTrans.position, Quaternion.identity, canvasTrans);
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
