using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public GameObject NoticeObject;

    private int timer=2;
    public void setNotice(string NoticeContent)
    {
        text.text= NoticeContent;
        NoticeObject.gameObject.SetActive(true);
        Invoke("hideNotice",timer);
    }

    public void hideNotice()
    {
        NoticeObject.gameObject.SetActive(false);

    }
    

}
