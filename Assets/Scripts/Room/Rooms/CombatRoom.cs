using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatRoom : AbstractRoom
{
    public override RoomType Type => RoomType.Combat;
    public override bool ShowExecButton => false;
    public override bool ExecuteOnEnter => !isDone;

    private bool isDone = false;

    private string combatScene;
    private List<TextAsset> datas = new List<TextAsset>();

    public CombatRoom(string combatSceneName, params TextAsset[] enemyDatas)
    {
        combatScene = combatSceneName;
        datas = new List<TextAsset>(enemyDatas);
    }

    public override AbstractRoom Copy() => new CombatRoom(combatScene, datas.ToArray());

    public override void Execute()
    {
        Debug.Log("战斗");
        if (datas.Count == 0) return;
        if (isDone) return;
        DataManager.NextEnemyData = datas[new System.Random().Next() % datas.Count];
        SceneManager.LoadScene(combatScene);
    }
}