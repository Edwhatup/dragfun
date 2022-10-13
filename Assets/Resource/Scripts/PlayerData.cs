using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{

    //public CardStore CardStore;

    public int playerCoins;
    public Dictionary<string, int> deck=new Dictionary<string, int>();
    [SerializeField]
    TextAsset playerData;


    void Awake()
    {   
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        string[] dataRow = playerData.text.Split('\n');
        foreach(var row in dataRow)
        {
            if (row.Length == 0) continue;
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if(rowArray[0]== "coins")
            {
                playerCoins = int.Parse(rowArray[1]);
            }

            else if(rowArray[0]=="card")
            {
                string name = rowArray[1];
                int cnt=int.Parse(rowArray[2]);
                deck[name]=cnt;
                //Debug.Log(rowArray[1]);
                //playerCardNames.Add(id);
                //playerCards[id]=int.Parse(rowArray[2]);

                //Debug.Log(rowArray[1]+","+rowArray[2]);
            }
            
            //else if(rowArray[0]=="Deck")
            //{
            //    //int id = int.Parse(rowArray[1]);
            //    //int num = int.Parse(rowArray[2]);

            //    //playerDeckIds.Add(id);
            //    //playerDeckCards[id] = num;

            //}
        }

        //playerCardNames=playerCardNames.Distinct().ToList();
        //playerDeckIds=playerDeckIds.Distinct().ToList();
        
    }

    public void SavePlayerData()
    {
        //string path = Application.dataPath+"Assets/Datas/playerdata.csv";

        //List<string> datas = new List<string>();
        //datas.Add("coins,"+playerCoins.ToString());

        //foreach(var id in playerCardNames)
        //{ 
        //    if(playerCards[id]!=0)
        //    {
        //        datas.Add("card,"+ id.ToString() +","+playerCards[id].ToString());
        //    }
            
        //}

        //foreach(var id in playerDeckIds)
        //{ 
        //    if(playerDeckCards[id]!=0)
        //    {
        //        datas.Add("deck,"+ id.ToString() +","+playerDeckCards[id].ToString());
        //    }
            
        //}

        //File.WriteAllLines(path,datas);

        //playerCardNames=playerCardNames.Distinct().ToList();


        //保存卡组
    }
        
}
