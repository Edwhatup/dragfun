using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{

    public class ZoomUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // Start is called before the first frame update

        public float zoomSize = 1.2f;

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            transform.localScale = new Vector3(zoomSize, zoomSize, 1.0f);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            transform.localScale = Vector3.one;
        }
    }
}