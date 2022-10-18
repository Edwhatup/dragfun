using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    //public Transform deckPanel;
    //public Transform libraryPanel;

    //public GameObject deckPrefab;



    //public GameObject DataManager;

    //private PlayerData PlayerData;
    //private CardStore CardStore;
    //private Dictionary<int,GameObject> libraryDic = new Dictionary<int, GameObject>();
    //private Dictionary<int,GameObject> deckDic = new Dictionary<int, GameObject>();



    //// Start is called before the first frame update
    //void Start()
    //{
    //    PlayerData = DataManager.GetComponent<PlayerData>();
    //    CardStore = DataManager.GetComponent<CardStore>();

    //    UpdateLibrary();

    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    //public void UpdateLibrary()
    //{
    //    foreach( var id in PlayerData.playerCardNames)
    //    {
    //        Debug.Log("读到卡：");
    //        Debug.Log(id);
    //        //CreateCard(id,CardState.Library);

    //    }

    //}


    //public void UpdateDeck()
    //{
    //    foreach( var id in PlayerData.playerDeckIds)
    //    {
    //        CreateCard(id, CardState.Deck);

    //    }
    //}

    //public void UpdateCard(CardState _state, int _id)
    //{
    //    if(_state == CardState.Deck)
    //    {
    //        PlayerData.playerDeckCards[_id]--;
    //        PlayerData.playerCards[_id]++;

    //        if(!deckDic[_id].GetComponent<CardCounter>().SetCounter(-1))
    //        {
    //            deckDic.Remove(_id);
    //        }


    //        if(libraryDic.ContainsKey(_id))
    //        {
    //            libraryDic[_id].GetComponent<CardCounter>().SetCounter(1);
    //        }
    //        else
    //        {
    //            CreateCard(_id,CardState.Library);
    //        }

    //    }
    //    else if (_state == CardState.Library)
    //    {
    //        PlayerData.playerDeckCards[_id]++;
    //        PlayerData.playerCards[_id]--;

    //        if(deckDic.ContainsKey(_id))
    //        {
    //            deckDic[_id].GetComponent<CardCounter>().SetCounter(1);
    //        }

    //        else
    //        {
    //            CreateCard(_id, CardState.Deck);
    //        }

    //        if(!libraryDic[_id].GetComponent<CardCounter>().SetCounter(-1))
    //        {
    //            libraryDic.Remove(_id);
    //        }
    //    }

    //}


    //public void CreateCard(int _id, CardState _CardState)
    //{

    //    Transform targetPanel;
    //    GameObject targetPrefab;
    //    var refData = PlayerData.playerCards;
    //    Dictionary<int,GameObject> targetDic = libraryDic;

    //    if(_CardState == CardState.Library)
    //    {
    //        targetPanel = libraryPanel;
    //        targetPrefab = deckPrefab;

    //    }
    //    else if(_CardState == CardState.Deck)
    //    {
    //        targetPanel = deckPanel;
    //        targetPrefab = deckPrefab;
    //        refData = PlayerData.playerDeckCards;
    //        targetDic = deckDic;
    //    }

    //    GameObject newCard = Instantiate(deckPrefab,libraryPanel);
    //    newCard.GetComponent<CardCounter>().SetCounter(refData[_id]);
    //    //newCard.GetComponent<CardDisplay>().card = CardStore.cardBox[_id];
    //    targetDic.Add(_id,newCard);
    //}
    

}
