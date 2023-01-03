using UnityEngine;
public class Arrow : MonoBehaviour
{
    private RectTransform arrow;
    void Start()
    {
        arrow = transform.GetComponent<RectTransform>();
    }
    void Update()
    {
        var dis = Input.mousePosition - transform.position;
        float len = dis.magnitude;
        float angle = Mathf.Atan2(dis.y, dis.x);
        arrow.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        arrow.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, len);
    }
}