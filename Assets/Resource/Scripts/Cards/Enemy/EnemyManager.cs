using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//控制对敌人的可视化和敌人的回合管理
public class EnemyManager : MonoSingleton<EnemyManager>,IGameTurn
{
    public List<Enemy> enemies=new List<Enemy>();
    [SerializeField]
    TextAsset enemyData;
    [SerializeField]
    RectTransform enemyBoardTrans;
    [SerializeField]
    GameObject enemyPrefab;
    Dictionary<Enemy, GameObject> enemyObjects = new Dictionary<Enemy, GameObject>();


    new void Awake()
    {
        base.Awake();
        LoadEnemyData();
        Debug.Log(enemies.Count);
    }
    public void LoadEnemyData()
    {
        string[] dataRow = enemyData.text.Split('\n');
        Debug.Log(enemyData.text);
        var types = GetAllEnemyTypes();
        foreach (var row in dataRow)
        {
            Debug.Log(row);
            string[] rowArray = row.Split(',');
            if (rowArray.Length<3 || rowArray[0] == "id")
            {
                continue;
            }
            string id = rowArray[0];
            string name = rowArray[1];  
            int hp=int.Parse(rowArray[2]);
            string[] paras = rowArray.Skip(2).ToArray();
            var ctor = types[id].GetConstructor(new Type[] { typeof(string), typeof(int), typeof(string[]) });
            Debug.Log(ctor);
            var enemy=ctor.Invoke(new object[] { name, hp, paras }) as Enemy;
            if(enemy==null)
            {
                Debug.LogError($"创建id={id}，name={name}的敌人失败");
                continue;
            }
            enemies.Add(enemy);
        }
    }

    public void CreateOrUpdateEnemyObjects()
    {
        foreach(var enemy in enemies)
        {
            if(enemy.state==BattleCard.BattleState.HalfDead || enemy.state==BattleCard.BattleState.Dead)
            {
                if (enemyObjects.ContainsKey(enemy))
                {
                    enemyObjects[enemy].GetComponent<EnemyDisplay>().OnDestory();
                    Destroy(enemyObjects[enemy]);
                }
            }
            else 
            {
                if (!enemyObjects.ContainsKey(enemy))
                    enemyObjects[enemy] = CreateEnemyObject(enemy);
                enemyObjects[enemy].GetComponent<EnemyDisplay>().UpdateVisual();
            }
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(enemyBoardTrans);
    }

    private GameObject CreateEnemyObject(Enemy enemy)
    {
        GameObject gameObject = GameObject.Instantiate(enemyPrefab,enemyBoardTrans);
        var enemyDisplay=gameObject.GetComponent<EnemyDisplay>();
        enemyDisplay.enemy = enemy;
        enemyDisplay.OnCreate();
        return gameObject;
    }

    private static Dictionary<string, Type> GetAllEnemyTypes()
    {
        var res = new Dictionary<string, Type>();
        var type = typeof(Enemy);
        var assembly = type.Assembly;
        var assemblyAllTypes = assembly.GetTypes();
        foreach (var t in assemblyAllTypes)
        {
            if (!t.IsAbstract && t.IsSubclassOf(type))
            {
                res.Add(t.Name, t);
            }
        }
        return res;
    }
    #region IGameTurn 实现部分
    public void GameStart()
    {
        CreateOrUpdateEnemyObjects();
    }

    public void PlayerAction()
    {

    }

    public void EnemyAction()
    {
    }

    public void PlayDraw()
    {

    }
    #endregion
}