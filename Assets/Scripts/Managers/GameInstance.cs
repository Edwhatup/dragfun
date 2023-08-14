using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance Instance { get; private set; }
    public static ShopPool ShopPool { get; private set; }

    static GameInstance()
    {
        if (Instance) return;
        GameObject inst = new GameObject("GameInst", typeof(GameInstance));
        GameObject.DontDestroyOnLoad(inst);

        // TODO: 在完成整个游戏后，删除自动启动
        inst.GetComponent<GameInstance>().StartGame();
    }

    public void StartGame()
    {
        ShopPool = new ShopPool();
    }
}