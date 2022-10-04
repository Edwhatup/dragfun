using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public enum CardState
{
    Library,Deck
}

public class ClickCard : MonoBehaviour,IPointerDownHandler
{
    // Start is called before the first frame update

    private DeckManager DeckManager;
    //private PlayerData PlayerData;

    public CardState state;


    void Start()
    {
        DeckManager = GameObject.Find("DataManager").GetComponent<DeckManager>();
        //PlayerData = GameObject.Find("DataManager").GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        int id = this.GetComponent<CardDisplay>().card.id;
        
        DeckManager.UpdateCard(state, id);
    }
}
