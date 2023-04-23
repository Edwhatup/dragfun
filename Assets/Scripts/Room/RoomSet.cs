using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RoomSet
{
    public Dictionary<Vector2Int, AbstractRoom> Rooms => rooms;
    private Dictionary<Vector2Int, AbstractRoom> rooms = new Dictionary<Vector2Int, AbstractRoom>();

    private int maxRooms;

    // 随机地牢生成参数: 向前/两边生成的概率；向前/两边生成概率的变化率
    private float towardWeight, sideWeight, towardSlope, sideSlope;

    private System.Random random;

    public RoomSet(int maxRooms, float towardWeight = 1, float sideWeight = 0.8f, float towardSlope = 0.6f, float sideSlope = 0.8f)
    {
        this.maxRooms = maxRooms;
        this.towardWeight = towardWeight;
        this.sideWeight = sideWeight;
        this.towardSlope = towardSlope;
        this.sideSlope = sideSlope;
        if (maxRooms <= 1)
        {
            Debug.LogError("错误: 地牢太小!");
            return;
        }

        random = new System.Random();

        // 创建初始房间
        AddRoom(Vector2Int.zero, new StartRoom());

        // 生成地牢
        Generate(Vector2Int.right, Direction.Right, towardWeight, sideWeight);

        // 替换终点
        var farest = Vector2Int.zero;
        foreach (var item in rooms)
        {
            if (item.Key.sqrMagnitude > farest.sqrMagnitude)
                farest = item.Key;
        }
        if (farest.sqrMagnitude > 0) ReplaceRoom(farest, new EndRoom());
    }

    public void PrintRooms()
    {
        StringBuilder sb = new StringBuilder("Roomset: \n");
        foreach (var item in rooms)
        {
            sb.Append($"{item.Key}: {item.Value.Type}\n");
        }
        Debug.Log(sb);
    }

    private void Generate(Vector2Int pos, Direction nextDir, float towardWeight, float sideWeight)
    {
        if (rooms.Count >= maxRooms) return;
        if (rooms.ContainsKey(pos)) return;

        CreateRoom(pos);

        // 左转
        if (random.Next() % 100 <= 100 * sideWeight) Generate(Move(pos, TurnLeft(nextDir)), TurnLeft(nextDir), towardWeight, sideWeight * sideSlope);
        // 右转
        if (random.Next() % 100 <= 100 * sideWeight) Generate(Move(pos, TurnRight(nextDir)), TurnRight(nextDir), towardWeight, sideWeight * sideSlope);
        // 直走
        if (random.Next() % 100 <= 100 * towardWeight) Generate(Move(pos, nextDir), nextDir, towardWeight * towardSlope, sideWeight);
    }

    private void CreateRoom(Vector2Int pos)
    {
        if (rooms.ContainsKey(pos)) return;

        rooms.Add(pos, new NullRoom());
    }

    private void AddRoom(Vector2Int pos, AbstractRoom room)
    {
        if (rooms.ContainsKey(pos)) ReplaceRoom(pos, room);
        else rooms.Add(pos, room);
    }

    private void RemoveRoom(Vector2Int pos)
    {
        if (rooms.ContainsKey(pos)) rooms.Remove(pos);
    }

    public void ReplaceRoom(Vector2Int pos, AbstractRoom room)
    {
        if (rooms.ContainsKey(pos))
            rooms[pos] = room;
        else Debug.LogError($"错误: {pos} 位置没有房间!");
    }

    private Direction TurnLeft(Direction d) => (int)d - 1 < 0 ? Direction.Left : (Direction)((int)d - 1);
    private Direction TurnRight(Direction d) => (int)d + 1 > 3 ? Direction.Up : (Direction)((int)d + 1);
    private Vector2Int Move(Vector2Int pos, Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: pos.y++; break;
            case Direction.Down: pos.y--; break;
            case Direction.Left: pos.x--; break;
            case Direction.Right: pos.x++; break;
        }
        return pos;
    }

    private enum Direction
    {
        Up = 0, Right = 1, Down = 2, Left = 3
    }
}
