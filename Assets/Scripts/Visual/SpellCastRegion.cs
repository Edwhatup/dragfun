using Card.Spell;
using Core;
using Seletion;
using UnityEngine;
using UnityEngine.UI;

namespace Visual
{
    public class SpellCastRegion : MonoBehaviour, ISeletable
    {
        Image image;

        public void Start()
        {
            image = GetComponent<Image>();
            Selections.Instance.AddCanSelection(this);
        }
        public void OnDestroy()
        {
            Selections.Instance?.RemoveCanSelection(this);
        }

        public void UpdateSelectableVisual()
        {
            if (Selections.Instance.CanSelect(this))
            {
                image.color = Color.green;
            }
            else image.color = Color.white;
        }

        void Update()
        {
            if (Selections.Instance.CanSelect(this))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    var rectTrans = GetComponent<RectTransform>();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                       rectTrans, Input.mousePosition, null, out Vector2 res);
                    bool isInRegion = rectTrans.rect.Contains(res);
                    if (isInRegion)
                        Selections.Instance.AddSelection(this);
                }
            }
        }
    }


}
