using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 StartPoint;
    public Vector2 EndPoint;

    private RectTransform arrow;

    private Vector2 CardPoint;

    private float ArrowLength;
    private float ArrowAngle;
    private Vector2 ArrowPosition;

    // Start is called before the first frame update
    void Start()
    {
        arrow =  transform.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //EndPoint = Input.mousePosition- new Vector3(Screen.width/2,Screen.height/2,0.0f);
        EndPoint = Input.mousePosition-new Vector3(CardPoint.x,CardPoint.y,0.0f);
        EndPoint = Vector3.Scale(EndPoint,new Vector3(0.92f,0.92f,0.0f));
    
        ArrowPosition = new Vector2((EndPoint.x+StartPoint.x)/2,(EndPoint.y+StartPoint.y)/2);
        ArrowLength = Mathf.Sqrt((EndPoint.x-StartPoint.x)*(EndPoint.x-StartPoint.x) + (EndPoint.y-StartPoint.y)*(EndPoint.y-StartPoint.y));
        ArrowAngle = Mathf.Atan2(EndPoint.y - StartPoint.y,EndPoint.x -StartPoint.x);

        arrow.localPosition = ArrowPosition;
        arrow.sizeDelta = new Vector2(ArrowLength , arrow.sizeDelta.y);
        arrow.localEulerAngles = new Vector3 (0.0f,0.0f,ArrowAngle*180/Mathf.PI);

    }

    public void SetStartPoint(Vector2 _startPoint)
    {
        //StartPoint = _startPoint - new Vector2(Screen.width/2,Screen.height/2);
        CardPoint = _startPoint;
        StartPoint = new Vector2(0,0);
    }

    
}
