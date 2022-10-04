using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[][] cells = new GameObject[4][];
    public GameObject cell00;
    public GameObject cell01;
    public GameObject cell02;
    public GameObject cell10;
    public GameObject cell11;
    public GameObject cell12;
    public GameObject cell20;
    public GameObject cell21;
    public GameObject cell22;
    public GameObject cell30;
    public GameObject cell31;
    public GameObject cell32;






    void Start()
    {
        cells[0] = new GameObject[]{cell00,cell01,cell02};
        cells[1] = new GameObject[]{cell10,cell11,cell12};
        cells[2] = new GameObject[]{cell20,cell21,cell22};
        cells[3] = new GameObject[]{cell30,cell31,cell32};

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> GetCellList()
    {
        List<GameObject> cellList = new List<GameObject>();
        foreach(var cellrow in cells)
        {
            foreach (var cell in cellrow)
            {
                cellList.Add(cell);
            }
        }

        return cellList;
    }

    public List<GameObject> GetAdjCells(GameObject _cell)
    {
        int row=0;
        int col=0;
        List<GameObject> adjCells = new List<GameObject>();
        for(int i=0; i<4 ;i++)
        {
            for(int j=0; j<3;j++)
            {
                if(cells[i][j]== _cell)
                {
                    row = i;
                    col = j; 
                    //Debug.Log("卡牌所在格： i："+i.ToString()+"j："+j.ToString());
                    break;
                }
            }
        }

        if(row-1>=0) adjCells.Add(cells[row-1][col]);
        if(row+1<=3) adjCells.Add(cells[row+1][col]);
        if(col-1>=0) adjCells.Add(cells[row][col-1]);
        if(col+1<=2) adjCells.Add(cells[row][col+1]);

        return adjCells;

    }

    public int getRange(GameObject _cell)
    {
        int row=0;
        int col=0;
        List<GameObject> adjCells = new List<GameObject>();
        for(int i=0; i<4 ;i++)
        {
            for(int j=0; j<3;j++)
            {
                if(cells[i][j]== _cell)
                {
                    row = i;
                    col = j; 
                    //Debug.Log("卡牌所在格： i："+i.ToString()+"j："+j.ToString());
                    break;
                }
            }
        }
        return row+1;
    }
}
