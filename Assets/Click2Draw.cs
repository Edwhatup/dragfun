using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click2Draw : MonoBehaviour
{
    [SerializeField] int drawCnt;
    public void Click2DrawCard()
    {
        if(GameManager.Instance.pp>=3)
        {
            CardManager.Instance.DrawCard(drawCnt);
            EventManager.Instance.PassEvent(new UsePPEvent(3));
            GameManager.Instance.Refresh();
        }
    }
}
