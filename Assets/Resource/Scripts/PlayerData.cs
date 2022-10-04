using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update

    public CardStore CardStore;
    public EnemyData EnemyData;

    public int playerCoins;

    public List<int> playerCardIds = new List<int>();
    public int[] playerCards = new int[30000];


    public List<int> playerDeckIds = new List<int>();
    public int[] playerDeckCards = new int[30000];


    public TextAsset playerData;


    void Awake()
    {   
        CardStore = CardStore.GetComponent<CardStore>();
        EnemyData = EnemyData.GetComponent<EnemyData>();
        EnemyData.LoadEnemyData();
        CardStore.LoadCardData();
        LoadPlayerData();

        //注释掉了CardStore的启动，统一在PlayerData这边启动加载，保证加载顺序正确。
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadPlayerData()
    {

        string[] dataRow = playerData.text.Split('\n');
        foreach(var row in dataRow)
        {
            if (row.Length == 0) continue;
            string[] rowArray = row.Split(',');
            //Debug.Log(rowArray.Length);

            if (rowArray[0] == "#")
            {
                //Debug.Log("On reading...");
                continue;
                
            }
            else if(rowArray[0]== "coins")
            {
                playerCoins = int.Parse(rowArray[1]);
                
            }

            else if(rowArray[0]=="card")
            {
                int id =int.Parse(rowArray[1]);
                //Debug.Log(rowArray[1]);
                playerCardIds.Add(id);
                playerCards[id]=int.Parse(rowArray[2]);

                //Debug.Log(rowArray[1]+","+rowArray[2]);
            }
            
            else if(rowArray[0]=="Deck")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);

                playerDeckIds.Add(id);
                playerDeckCards[id] = num;

            }
        }

        playerCardIds=playerCardIds.Distinct().ToList();
        playerDeckIds=playerDeckIds.Distinct().ToList();
        
    }

    public void SavePlayerData()
    {
        string path = Application.dataPath+"Assets/Datas/playerdata.csv";

        List<string> datas = new List<string>();
        datas.Add("coins,"+playerCoins.ToString());

        foreach(var id in playerCardIds)
        { 
            if(playerCards[id]!=0)
            {
                datas.Add("card,"+ id.ToString() +","+playerCards[id].ToString());
            }
            
        }

        foreach(var id in playerDeckIds)
        { 
            if(playerDeckCards[id]!=0)
            {
                datas.Add("deck,"+ id.ToString() +","+playerDeckCards[id].ToString());
            }
            
        }

        File.WriteAllLines(path,datas);

        playerCardIds=playerCardIds.Distinct().ToList();


        //保存卡组
    }
        
}
