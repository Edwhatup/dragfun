//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class EnemyData : MonoBehaviour
//{
//    public TextAsset enemyData;
//    public List<Enemy> enemies;
//    public Dictionary<Enemy,GameObject> enemyObjects; 
//    void Awake()
//    {
//        enemies = new List<Enemy>();
//        enemyObjects = new Dictionary<Enemy,GameObject>();
//        LoadEnemyData();
//    }
//    public void LoadEnemyData()
//    {
//        string[] dataRow = enemyData.text.Split('\n');

//        foreach(var row in dataRow)
//        {
//            string[] rowArray = row.Split(',');
//            if (rowArray[0] == "#")
//            {
//                //Debug.Log("On reading...");
//                continue;
                
//            }
//            else if(rowArray[0]== "normal")
//            {
//                //new normal enemy
//                int id = int.Parse(rowArray[1]);
//                string name = rowArray[2];
//                int hp = int.Parse(rowArray[3]);

//                NormalEnemy normalEnemy = new NormalEnemy(id,name,hp);
//                enemyIdList.Add(id);
//                allEnemyBox[id]=normalEnemy;

//                //Debug.Log("新id：");
//                //Debug.Log(id);

//                //Debug.Log("new enemy:"+ normalEnemy.enemyName);

//            }
//            else if(rowArray[0]== "boss")
//            {
//                //new spell card
//                //new normal enemy
//                int id = int.Parse(rowArray[1]);
//                string name = rowArray[2];
//                int hp = int.Parse(rowArray[3]);
//                BossEnemy bossEnemy = new BossEnemy(id,name,hp);
//                enemyIdList.Add(id);
//                allEnemyBox[id]=bossEnemy;


//            }

//        }


//    }
//    public void CreateOrUpdateEnemyObjects()
//    {

//    }
//}
