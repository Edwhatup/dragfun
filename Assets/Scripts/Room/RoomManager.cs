using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public bool initOnStart;
    public Transform roomParent;
    public RoomCharactor charactor;
    public Button interactButton;

    [Header("战斗场景的数据")]
    public string combatSceneName;
    public TextAsset enemyData;

    [Header("控制地图生成的参数")]
    public int maxRooms = 10;
    public float towardsW = 1, towardsS = 0.6f;
    public float sideW = 0.8f, sideS = 0.8f;

    [Header("各个房间的预制体")]
    public float interval = 2;
    public GameObject nullRoom, startRoom, endRoom, combatRoom, bossRoom, shopRoom;

    public static RoomSet RoomSet { get; private set; }
    public static Vector2Int CurPos { get; private set; }

    private void Start()
    {
        if (RoomSet == null) BuildMap(maxRooms, towardsW, towardsS, sideW, sideS);
        ShowMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            if (RoomSet.HasPos(CurPos + Vector2Int.left)
                && charactor.MoveTo((Vector2)(CurPos + Vector2Int.left) * interval))
                Enter(CurPos + Vector2Int.left);

        if (Input.GetKeyDown(KeyCode.S))
            if (RoomSet.HasPos(CurPos + Vector2Int.down)
                && charactor.MoveTo((Vector2)(CurPos + Vector2Int.down) * interval))
                Enter(CurPos + Vector2Int.down);

        if (Input.GetKeyDown(KeyCode.D))
            if (RoomSet.HasPos(CurPos + Vector2Int.right)
                && charactor.MoveTo((Vector2)(CurPos + Vector2Int.right) * interval))
                Enter(CurPos + Vector2Int.right);

        if (Input.GetKeyDown(KeyCode.W))
            if (RoomSet.HasPos(CurPos + Vector2Int.up)
                && charactor.MoveTo((Vector2)(CurPos + Vector2Int.up) * interval))
                Enter(CurPos + Vector2Int.up);
    }

    public void BuildMap(int maxRooms, float towardsW, float towardsS, float sideW, float sideS)
    {
        RoomSet = new RoomSet.Builder(maxRooms)
            .Forward(towardsW, towardsS)
            .Left(sideW, sideS)
            .Right(sideW, sideS)
            .Default(new CombatRoom(combatSceneName, enemyData))
            .Random(new NullRoom(), 5, 0.3f)
            .Build();

        CurPos = Vector2Int.zero;
    }

    public void ShowMap()
    {
        foreach (var item in RoomSet.Rooms)
        {
            switch (item.Value.Type)
            {
                case RoomType.Null:
                    Instantiate<GameObject>(nullRoom, ((Vector2)item.Key) * interval, Quaternion.identity, roomParent);
                    break;

                case RoomType.Start:
                    Instantiate<GameObject>(startRoom, ((Vector2)item.Key) * interval, Quaternion.identity, roomParent);
                    break;

                case RoomType.End:
                    Instantiate<GameObject>(endRoom, ((Vector2)item.Key) * interval, Quaternion.identity, roomParent);
                    break;

                case RoomType.Combat:
                    Instantiate<GameObject>(combatRoom, ((Vector2)item.Key) * interval, Quaternion.identity, roomParent);
                    break;

                case RoomType.Boss:
                    Instantiate<GameObject>(bossRoom, ((Vector2)item.Key) * interval, Quaternion.identity, roomParent);
                    break;

                case RoomType.Shop:
                    Instantiate<GameObject>(shopRoom, ((Vector2)item.Key) * interval, Quaternion.identity, roomParent);
                    break;
            }
        }

        charactor.transform.position = (Vector2)CurPos * interval;
    }

    private void Enter(Vector2Int pos)
    {
        if (!RoomSet.HasPos(pos)) return;
        CurPos = pos;

        var room = RoomSet.Rooms[CurPos];

        if (room.ExecuteOnEnter)
            room.Execute();

        interactButton.onClick.RemoveAllListeners();
        if (room.ShowExecButton)
        {
            interactButton.gameObject.SetActive(true);
            interactButton.onClick.AddListener(room.Execute);
        }
        else interactButton.gameObject.SetActive(false);
    }

    [Serializable]
    public class TextAssetWeightPair
    {
        public TextAsset text;
        public int weight;
    }
}