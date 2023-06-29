using UnityEngine;

public class RoomSetTest : MonoBehaviour
{
    public GameObject prefab;
    public float distance = 64;

    [Header("控制地图生成的参数")]
    public int maxRooms = 10;
    public float towardsW = 1, towardsS = 0.6f;
    public float sideW = 0.8f, sideS = 0.8f;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);

        var r = new RoomSet.Builder(maxRooms)
                        .Forward(towardsW, towardsS)
                        .Left(sideW, sideS)
                        .Right(sideW, sideS)
                        // .Default(new CombatRoom())
                        .Random(new NullRoom(),5,0.3f)
                        .Build();
        foreach (var item in r.Rooms)
        {
            var g = Instantiate(prefab, transform.position + (Vector3)(Vector2)item.Key * distance, Quaternion.identity, transform);
            if (item.Value.Type == RoomType.Start) g.GetComponent<SpriteRenderer>().color = Color.green;
            if (item.Value.Type == RoomType.End) g.GetComponent<SpriteRenderer>().color = Color.red;
            if (item.Value.Type == RoomType.Combat) g.GetComponent<SpriteRenderer>().color = Color.yellow;
        }

    }
}