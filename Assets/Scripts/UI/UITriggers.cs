using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UITriggers : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent onClick;

    private float timer;
    private bool holding = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        timer = 0;
        holding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (timer < 0.1f) onClick.Invoke();
    }

    private void Update()
    {
        if (holding)
            timer += Time.deltaTime;
    }
}
