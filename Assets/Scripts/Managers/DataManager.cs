using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager:MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    [SerializeField]
    TextAsset enemyData;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(Instance);
    }

    public string CurrentEnemyData=>enemyData.text;
}
