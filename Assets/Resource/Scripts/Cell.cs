using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public bool movable;
    
    public GameObject card;

    public GameObject SummonCell;




    // Start is called before the first frame update
    void Start()
    {
        card = GameObject.FindGameObjectWithTag("Card");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("现在格子内card：");
        //Debug.Log(card);
        if(SummonCell.activeInHierarchy == true)
        {
            if(GameManager.Instance.GetWaitingCardState() == 1 ) 
            {
                // BattleManager.Instance.SummonConfirm(gameObject);
                //Debug.Log("summon");
            }
            
            else if(GameManager.Instance.GetWaitingCardState() == 2)
            {
                //BattleManager.Instance.OnBoardConfirm(gameObject);
                //Debug.Log("onboard");
            }
            
            
        }
        

    }
}
