using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click2Draw : MonoBehaviour
{
    [SerializeField] int drawCnt;
    public void Click2DrawCard()
    {
        //测试方便我先改成1了
        if(GameManager.Instance.pp>=1)
        {
            CardManager.Instance.DrawCard(drawCnt);
            EventManager.Instance.PassEvent(new UsePPEvent(1));
            GameManager.Instance.Refresh();
        }
    }
}
