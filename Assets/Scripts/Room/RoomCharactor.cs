using UnityEngine;
using System.Collections;

public class RoomCharactor : MonoBehaviour
{
    public float speed;

    private bool isMoving = false;

    public bool MoveTo(Vector2 pos)
    {
        if (isMoving) return false;
        StartCoroutine(_MoveTo(pos));
        return true;
    }

    private IEnumerator _MoveTo(Vector2 pos)
    {
        isMoving = true;
        while (((Vector2)transform.position - pos).sqrMagnitude > 1e-3)
        {
            yield return 0;
            transform.position = Vector3.Lerp(transform.position, pos, speed);
        }
        transform.position = pos;
        isMoving = false;
    }
}