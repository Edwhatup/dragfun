using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;
    public Card[] allCardBox = new Card[30000];
    public List<int> cardIdList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        LoadCardData();
        //TestLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCardData()
    {
        string[] dataRow = cardData.text.Split('\n');

        foreach(var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                //Debug.Log("On reading...");
                continue;
                
            }
            else if(rowArray[0]== "monster")
            {
                //new monster card
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int atk = int.Parse(rowArray[3]);
                int hp = int.Parse(rowArray[4]);
                string eff = rowArray[5];

                MonsterCard monsterCard = new MonsterCard(id,name,atk,hp,eff);

                cardIdList.Add(id);
                allCardBox[id]=monsterCard;

                //Debug.Log("新id：");
                //Debug.Log(id);

                //Debug.Log("new monster card:"+ monsterCard.cardName);

            }
            else if(rowArray[0]== "spell")
            {
                //new spell card
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                string eff = rowArray[3];

                SpellCard spellCard = new SpellCard(id,name,eff);


                cardIdList.Add(id);
                allCardBox[id]=spellCard;

                //Debug.Log("新id：");
                //Debug.Log(id);

            }

        }

        cardIdList=cardIdList.Distinct().ToList();

        //Debug.Log(Random.Range(0,cardIdList.Count));
        //Debug.Log(allCardBox[cardIdList[Random.Range(0,cardIdList.Count)]].cardName);
        

    }

    public void TestLoad()
    {
        foreach (var card in allCardBox)
        {
            Debug.Log("编号"+card.id.ToString()+":"+card.cardName);
        }
    }

    public Card RandomCard()
    {
        int id = cardIdList[Random.Range(0,cardIdList.Count)];
        Card card = allCardBox[id];
        
        return card;
    }

    public Card CopyCard(int _id)
    {
        Card copyCard = new Card(_id,allCardBox[_id].cardName);
        if(allCardBox[_id] is MonsterCard)
        {
            var monstercard = allCardBox[_id] as MonsterCard;
            copyCard = new MonsterCard(_id,monstercard.cardName,monstercard.attack,monstercard.healthPointMax,monstercard.effect);
        }

        else if(allCardBox[_id] is SpellCard)
        {
            var spellcard = allCardBox[_id] as SpellCard;
            copyCard = new SpellCard(_id,spellcard.cardName,spellcard.effect);
        }

        return copyCard;
    }
}
