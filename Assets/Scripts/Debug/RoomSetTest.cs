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

        var r = new RoomSet(maxRooms, towardsW, sideW, towardsS, sideS);
        foreach (var item in r.Rooms)
        {
            var g = Instantiate(prefab, transform.position + (Vector3)(Vector2)item.Key * distance, Quaternion.identity, transform);
            if (item.Value.Type == RoomType.Start) g.GetComponent<SpriteRenderer>().color = Color.green;
            if (item.Value.Type == RoomType.End) g.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}