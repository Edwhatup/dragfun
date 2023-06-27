using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 0.1f;

    private void Update()
    {
        var delta = Vector3.Lerp(transform.position, target.position, speed);
        delta.z = transform.position.z;
        transform.position = delta;
    }
}
