using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class EnemyIntention : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    EnemyVisual visual;
    [SerializeField]
    GameObject intention;
    [SerializeField]
    Text text;
    public void OnPointerEnter(PointerEventData eventData)
    {
        intention.SetActive(true);
        //text.text = visual.card.GetIntention();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        intention.SetActive(false);
    }
}