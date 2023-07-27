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
    private TextAsset data;

    public CombatRoom(string combatSceneName, TextAsset enemyData)
    {
        combatScene = combatSceneName;
        data = enemyData;
    }

    public override AbstractRoom Copy() => new CombatRoom(combatScene, data);

    public override void Execute()
    {
        Debug.Log("战斗");
        if (isDone) return;
        DataManager.NextEnemyData = data;
        SceneManager.LoadScene(combatScene);
    }
}