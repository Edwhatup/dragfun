using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour,IPointerDownHandler,IUpdateVisual,ISeletable
{
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text healthText;
    [SerializeField]
    Text descText;
    [SerializeField]
    GameObject selectableEdge;
    public Enemy enemy;


    public int TargetCount =>0;

    public void OnCreate()
    {
        Selections.Instance.AddCanSelection(this);
    }
    public void OnDestory()
    {
        Selections.Instance.RemoveCanSelection(this);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
             
    }

    public bool CanSelect()
    {
        return true;
    }

    public void UpdateSelectableVisual()
    {
        if(CanSelect())
            selectableEdge.SetActive(true);
        else
            selectableEdge.SetActive(false);
    }

    public void UpdateVisual()
    {
        if (descText) descText.text = enemy.GetDesc();
        if (healthText) healthText.text = enemy.healthPoint+"/"+enemy.healthPointMax;
        if (nameText) nameText.text = enemy.name;
    }
}
