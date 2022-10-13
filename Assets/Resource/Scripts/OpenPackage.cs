using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPackage : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject cardPrefab;
    public GameObject cardPool;

    public int chooseCardNumber;

    CardStore CardStore;
    List<GameObject> cards = new List<GameObject>();

    public GameObject playerData;
    private PlayerData Data;


    void Start()
    {
        chooseCardNumber=5;

        Data = playerData.GetComponent<PlayerData>();
        CardStore =  GetComponent<CardStore>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnclickOpen()
    {
        ClearPool();

        for (int i=0; i<chooseCardNumber ;i++)
        {
            GameObject newCard = GameObject.Instantiate(cardPrefab,cardPool.transform);
            newCard.GetComponent<CardDisplay>().card = CardStore.RandomCard();
            cards.Add(newCard);
        }
        SaveCardData();
        //PlayerData.SavePlayerData();
        
    }

    public void ClearPool()
    {
        foreach(var card in cards)
        {
            Destroy(card);
        }

        cards.Clear();
    }


    public void SaveCardData()
    {
        foreach(var card in cards)
        {
            //int id = card.GetComponent<CardDisplay>().card.id;
            ////Debug.Log("保存读取id："+id.ToString());
            //Data.playerCardNames.Add(id);
            //Data.playerCards[id] += 1;
            //Debug.Log(id+"count:"+Data.playerCards[id]);

        }
    }
}
