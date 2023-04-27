using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RoomSet
{
    public Dictionary<Vector2Int, AbstractRoom> Rooms => rooms;
    private Dictionary<Vector2Int, AbstractRoom> rooms = new Dictionary<Vector2Int, AbstractRoom>();

    public RoomSet(Builder builder)
    {
        rooms = builder.rooms;
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

    public class Builder
    {
        protected int maxRooms;
        protected float FWeight = 0.8f, FSlope = 0.6f,
                        LWeight = 0.6f, LSlope = 0.2f,
                        RWeight = 0.6f, RSlope = 0.2f;
        protected internal Dictionary<Vector2Int, AbstractRoom> rooms = new Dictionary<Vector2Int, AbstractRoom>();
        private Direction startDirection = Direction.Right;
        private BuildType type = BuildType.Simple;

        private System.Random random;

        public Builder(int maxRooms = 2)
        {
            this.maxRooms = maxRooms;
        }

        /// <summary>
        /// 
        /// </summary>
        public Builder Forward(float weight, float slope)
        {
            FWeight = weight;
            FSlope = slope;
            return this;
        }

        public Builder Left(float weight, float slope)
        {
            LWeight = weight;
            LSlope = slope;
            return this;
        }

        public Builder Right(float weight, float slope)
        {
            RWeight = weight;
            RSlope = slope;
            return this;
        }

        public Builder Towards(Direction dir)
        {
            startDirection = dir;
            return this;
        }

        public Builder RandomRoom(AbstractRoom room, int maxCount = 1, float possibility = 0.3f, int minDistance = 1, int maxDistance = 100)
        {
            return this;
        }

        public Builder Simple()
        {
            type = BuildType.Simple;
            return this;
        }

        public RoomSet Build()
        {
            if (maxRooms <= 1)
            {
                Debug.LogError("错误: 地牢太小!");
                return null;
            }

            random = new System.Random();

            // 创建初始房间
            AddRoom(Vector2Int.zero, new StartRoom());

            switch (type)
            {
                case BuildType.Simple:
                    // 生成地牢
                    Generate(Vector2Int.right, Direction.Right, FWeight, LWeight, RWeight);

                    // 替换终点
                    var farest = Vector2Int.zero;
                    foreach (var item in rooms)
                    {
                        if (item.Key.sqrMagnitude > farest.sqrMagnitude)
                            farest = item.Key;
                    }
                    if (farest.sqrMagnitude > 0) ReplaceRoom(farest, new EndRoom());

                    return new RoomSet(this);
            }
            return null;
        }

        private void Generate(Vector2Int pos, Direction nextDir, float fw, float lw, float rw)
        {
            if (rooms.Count >= maxRooms) return;
            if (rooms.ContainsKey(pos)) return;

            CreateRoom(pos);

            // 左转
            if (random.Next() % 100 <= 100 * lw) Generate(Move(pos, TurnLeft(nextDir)), TurnLeft(nextDir), fw, lw * LSlope, RWeight);
            // 右转
            if (random.Next() % 100 <= 100 * rw) Generate(Move(pos, TurnRight(nextDir)), TurnRight(nextDir), fw, lw, rw * RSlope);
            // 直走
            if (random.Next() % 100 <= 100 * fw) Generate(Move(pos, nextDir), nextDir, fw * FSlope, lw, rw);
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

        private void ReplaceRoom(Vector2Int pos, AbstractRoom room)
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

        private enum BuildType
        {
            Simple
        }
    }

    public enum Direction
    {
        Up = 0, Right = 1, Down = 2, Left = 3
    }
}
