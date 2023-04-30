using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public static string NextEnemyDataPath { get; set; } = "";
    // private string nextEnemyDataPath = "";

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

        if (NextEnemyDataPath != "") enemyData = Resources.Load<TextAsset>(NextEnemyDataPath);
    }

    public string CurrentEnemyData => enemyData.text;
}
