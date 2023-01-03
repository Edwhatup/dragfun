using UnityEngine;
using UnityEngine.EventSystems;

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