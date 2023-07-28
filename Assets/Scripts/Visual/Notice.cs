using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    private static Notice instance;
    public static Notice Instance 
    {
        get{return instance;}
    }
    public Text text;
    public GameObject NoticeObject;

    private int timer=2;

    private void Awake() 
    {
<<<<<<< Updated upstream
=======
        NoticeObject.gameObject.SetActive(false);
>>>>>>> Stashed changes
        instance = this;
    }
        
    
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
