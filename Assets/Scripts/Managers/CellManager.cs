using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace Core
{
    public class CellManager : MonoSingleton<CellManager>
    {
        public List<List<Cell>> cells = new List<List<Cell>>();
        new void Awake()
        {
            base.Awake();
            GetCellLi();
        }


        // Update is called once per frame
        void Update()
        {

        }
        void GetCellLi()
        {
            int cnt = 0;
            foreach (Transform trans in transform)
            {
                if (cnt % 4 == 0) cells.Add(new List<Cell>());
                cells[cnt / 4].Add(trans.GetComponent<Cell>());
                cnt++;
            }
        }
        public List<GameObject> GetCellList()
        {
            List<GameObject> cellList = new List<GameObject>();
            foreach (var cellrow in cells)
            {
                foreach (var cell in cellrow)
                {
                    //cellList.Add(cell);
                }
            }

            return cellList;
        }

        public List<GameObject> GetAdjCells(GameObject _cell)
        {
            int row = 0;
            int col = 0;
            List<GameObject> adjCells = new List<GameObject>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cells[i][j] == _cell)
                    {
                        row = i;
                        col = j;
                        //Debug.Log("卡牌所在格： i："+i.ToString()+"j："+j.ToString());
                        break;
                    }
                }
            }

            //if(row-1>=0) adjCells.Add(cells[row-1][col]);
            //if(row+1<=3) adjCells.Add(cells[row+1][col]);
            //if(col-1>=0) adjCells.Add(cells[row][col-1]);
            //if(col+1<=2) adjCells.Add(cells[row][col+1]);

            return adjCells;

        }
        //获取两个cell之间的街道距离
        public int CellDistance(Cell cell1, Cell cell2)
        {
            var pos1 = GetCellPos(cell1);
            var pos2 = GetCellPos(cell2);
            return Mathf.Abs(pos1.Item1 - pos2.Item1) + Mathf.Abs(pos1.Item2 - pos2.Item2);
        }
        (int, int) GetCellPos(Cell cell)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells[i].Count; j++)
                {
                    if (ReferenceEquals(cells[i][j], cell)) return (i, j);
                }
            }
            return (-1, -1);
        }
        public int getRange(GameObject _cell)
        {
            int row = 0;
            int col = 0;
            List<GameObject> adjCells = new List<GameObject>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cells[i][j] == _cell)
                    {
                        row = i;
                        col = j;
                        //Debug.Log("卡牌所在格： i："+i.ToString()+"j："+j.ToString());
                        break;
                    }
                }
            }
            return row + 1;
        }
    }
}