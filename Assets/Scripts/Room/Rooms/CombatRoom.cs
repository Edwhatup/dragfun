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
    private List<string> paths = new List<string>();

    public CombatRoom(string combatSceneName, params string[] enemyDataPaths)
    {
        combatScene = combatSceneName;
        paths = new List<string>(enemyDataPaths);
    }

    public override AbstractRoom Copy() => new CombatRoom(combatScene, paths.ToArray());

    public override void Execute()
    {
        Debug.Log("战斗");
        if (paths.Count == 0) return;
        if (isDone) return;
        DataManager.NextEnemyDataPath = paths[new System.Random().Next() % paths.Count];
        SceneManager.LoadScene(combatScene);
    }
}