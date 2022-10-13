//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;

//public enum BattleCardState
//{
//    inHand, onBoard
//}


//public class BattleCard : MonoBehaviour,IPointerDownHandler,ISeletable,IUpdateVisual
//{
//    public BattleCardState state = BattleCardState.inHand;
//    public GameObject nowCell; 
//    public int atkRange=1;

//    public int TargetCount =>1;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {   if(BattleManager.Instance.GetWaitingCardState() == 0)
//        {
//            if((GetComponent<CardDisplay>().card is BattleCard) & (state == BattleCardState.inHand))
//            {
//                BattleManager.Instance.SummonRequest(gameObject);
//            }
//            else if((GetComponent<CardDisplay>().card is BattleCard) & (state == BattleCardState.onBoard))
//            {
//                BattleManager.Instance.OnBoardRequest(gameObject);
//            }
//        }
//        //点击手牌发起召唤请求

//        //场上点击产生移动、攻击请求
//    }

//    public bool CanSelect()
//    {
//        return true;
//    }

//    public void UpdateSelectableVisual()
//    {
//        throw new System.NotImplementedException();
//    }

//    public void UpdateVisual()
//    {

//    }
//}
