using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeEnergyText : MonoBehaviour
{

    // Start is called before the first frame update
    private static LifeEnergyText instance;
    public static LifeEnergyText Instance 
    {
        get{return instance;}
    }
    public Text text;
    public GameObject LifeEnergyObject;
    int lifeEnergyPoint;


    private void Awake() 
    {
        LifeEnergyObject.gameObject.SetActive(false);
        instance = this;
    }
        
    
    public void ShowText()
    {
        LifeEnergyObject.gameObject.SetActive(true);
    }

    public void HideText()
    {
        LifeEnergyObject.gameObject.SetActive(false);

    }
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text =  GameManager.Instance.LifeEnergyPoint.ToString();
    }
}
