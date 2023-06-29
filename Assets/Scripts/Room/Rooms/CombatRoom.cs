using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatRoom : AbstractRoom
{
    public override RoomType Type => RoomType.Combat;
    public override bool ShowExecButton => false;
    public override bool ExecuteOnEnter => !isDone;

    private bool isDone = false;

    private List<string> paths = new List<string>();

    public CombatRoom(params string[] enemyDataPaths)
    {
        paths = new List<string>(enemyDataPaths);
    }

    public override AbstractRoom Copy() => new CombatRoom();

    public override void Execute()
    {
        Debug.Log("战斗");
        if (paths.Count == 0) return;
        if (isDone) return;
        DataManager.NextEnemyDataPath = paths[new System.Random().Next() % paths.Count];
    }
}