using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public TextAsset enemyData;
    public Enemy[] allEnemyBox = new Enemy[30000];
    public List<int> enemyIdList = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        LoadEnemyData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEnemyData()
    {
        string[] dataRow = enemyData.text.Split('\n');

        foreach(var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                //Debug.Log("On reading...");
                continue;
                
            }
            else if(rowArray[0]== "normal")
            {
                //new normal enemy
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int hp = int.Parse(rowArray[3]);

                NormalEnemy normalEnemy = new NormalEnemy(id,name,hp);
                enemyIdList.Add(id);
                allEnemyBox[id]=normalEnemy;

                //Debug.Log("新id：");
                //Debug.Log(id);

                //Debug.Log("new enemy:"+ normalEnemy.enemyName);

            }
            else if(rowArray[0]== "boss")
            {
                //new spell card
                //new normal enemy
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int hp = int.Parse(rowArray[3]);

                BossEnemy bossEnemy = new BossEnemy(id,name,hp);
                enemyIdList.Add(id);
                allEnemyBox[id]=bossEnemy;


                //Debug.Log("新id：");
                //Debug.Log(id);

            }

        }

        enemyIdList=enemyIdList.Distinct().ToList();

        //Debug.Log(Random.Range(0,cardIdList.Count));
        //Debug.Log(allCardBox[cardIdList[Random.Range(0,cardIdList.Count)]].cardName);
        

    }
}
