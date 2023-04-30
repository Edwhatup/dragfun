using System;
using System.Collections.Generic;

public class CombatRoom : AbstractRoom
{
    public override RoomType Type => RoomType.Combat;

    private List<string> paths = new List<string>();

    public CombatRoom(params string[] enemyDataPaths)
    {
        paths = new List<string>(enemyDataPaths);
    }

    public override AbstractRoom Copy() => new CombatRoom();

    public override void Execute()
    {
        if (paths.Count == 0) return;
        DataManager.NextEnemyDataPath = paths[new Random().Next() % paths.Count];
    }
}