using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click2Draw : MonoBehaviour
{
    [SerializeField] int drawCnt;
    public void Click2DrawCard()
    {
        Quaternion
        CardManager.Instance.DrawCard(drawCnt);
    }
}
