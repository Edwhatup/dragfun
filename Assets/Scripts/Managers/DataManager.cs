using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public static TextAsset NextEnemyData { get; set; }
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

        if (NextEnemyData) enemyData = NextEnemyData;
        // if (NextEnemyData != "") enemyData = Resources.Load<TextAsset>(NextEnemyData);
    }

    public string CurrentEnemyData => enemyData.text;
}
