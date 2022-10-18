using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

public class Piece : MonoBehaviour
{
    public GameObject[] Cells;
    public GameObject player;

    public bool OnClick = false;

    // Start is called before the first frame update
    void Start()
    {
        Cells = GameObject.FindGameObjectsWithTag("Cell");
        player = GameObject.FindGameObjectWithTag("Card");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    private void OnMouseDown()
    {
        if (OnClick == false)
        {
            Debug.Log("点上了！");
            foreach (var cell in Cells)
            {
                if(Mathf.Abs(transform.position.x - cell.transform.position.x) + Mathf.Abs(transform.position.y - cell.transform.position.y) <=3 )
                {
                    cell.GetComponent<Cell>().movable = true;
                    cell.transform.localScale = new Vector3(0.29f,0.19f,0.85f);
                }
            }
            OnClick = true;
        }
        else
        {
            Debug.Log("不点了！");
            foreach (var cell in player.GetComponent<Piece>().Cells)
            {
                cell.GetComponent<Cell>().movable=false;
                cell.transform.localScale = new Vector3(0.25f,0.17f,0.85f);
            }
            OnClick = false ;
        }
    }

}

    
