using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour,IManager
{
    public static CellManager Instance { get; private set; }


    List<Cell> cells = new List<Cell>();
    [SerializeField]
    GameObject cellPrefab;
    [SerializeField]
    int row = 4;
    [SerializeField]
    int col = 4;
    [SerializeField]
    bool autoCreate=true;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }



    void InitCellList()
    {
        if(autoCreate)
        {
            cells.Clear();
            var grid = GetComponent<GridLayoutGroup>();
            var width = grid.cellSize.x * row + grid.spacing.x * (row - 1);
            var height = grid.cellSize.y * col + grid.spacing.y * (col - 1);
            var rect = GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    var go=GameObject.Instantiate(cellPrefab,transform);
                    var cell = go.GetComponent<Cell>();
                    cell.row = i;
                    cell.col = j;
                    cells.Add(cell);
                }
            }
        }
        else
        {
            int rowCnt = 0;
            int colCnt = 0;
            foreach(Transform trans in transform)
            {
                trans.GetComponent<Cell>().row = rowCnt;
                trans.GetComponent<Cell>().col = colCnt;
                colCnt++;
                rowCnt += colCnt / col;
                colCnt = colCnt % col;
            }
        }
    }

    #region 获取状态的方法 
    /// <summary>
    /// 获取符合条件的第一个Cell,没有符合条件的Cell时返回null
    /// </summary>
    /// <param name="condition"></param>
    public Cell GetSpecifyCell(Func<Cell, bool> condition)
    {
        return cells.Find((c) => condition(c));
    }
    /// <summary>
    /// 随机获取满足条件的所有Cell
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Cell> GetAllSpecifyCells(Func<Cell, bool> condition)
    {
        return cells.FindAll((c) => condition(c));
    }
    /// <summary>
    /// 随机获取满足条件的n个Cell,现有Cell数目不够n时返回所有满足条件的Cell 
    /// </summary>
    /// <param name="condition">条件</param>
    public List<Cell> GetRandomSpecifyCells(Func<Cell, bool> condition, int n)
    {
        return cells.FindAll(c => condition(c))
                    .GetRandomItems(n);
    }
    /// <summary>
    /// 随机获取满足条件的1个Cell,没有满足条件Cell时返回null
    /// </summary>
    /// <param name="condition">条件</param>
    public Cell GetRandomSpecifyEnemy(Func<Cell, bool> condition)
    {
        return cells.FindAll(c => condition(c))
                    .GetRandomItem();
    }
    /// <summary>
    /// 获取符合比较条件的最大Cell
    /// </summary>
    /// <param name="comparer">比较条件</param>
    public Cell GetMaxCell(Func<Cell, Cell, int> comparer)
    {
        return cells.GetMaxItem(comparer);
    }
    /// <summary>
    /// 获取符合比较条件的最小Cell
    /// </summary>
    /// <param name="comparer">比较条件</param>
    public Cell GetMinCell(Func<Cell, Cell, int> comparer)
    {
        return cells.GetMinItem(comparer);
    }
    #endregion
    public int GetChessDistance(Cell cell1, Cell cell2)
    {
        if (cell1 == null || cell2 == null) return -1;
        return Mathf.Max(Mathf.Abs(cell1.row - cell2.row), Mathf.Abs(cell1.col - cell2.col));
    }
    public int GetStreetDistance(Cell cell1, Cell cell2)
    {
        if (cell1 == null || cell2 == null) return -1;
        return Mathf.Abs(cell1.row - cell2.row) + Mathf.Abs(cell1.col - cell2.col);
    }
    public int GetRowDistance(Cell cell1, Cell cell2)
    {
        if (cell1 == null || cell2 == null) return -1;
        return Mathf.Abs(cell1.row - cell2.row);
    }
    public int GetColDistance(Cell cell1, Cell cell2)
    {
        if (cell1 == null || cell2 == null) return -1;
        return Mathf.Abs(cell1.col - cell2.col);
    }

    public int GetRowCount()
    {
        return row;
    }
    public int GetColCount()
    {
        return col;
    }

    public List<Cell> GetCells()
    {
        return cells;
    }
    public void Refresh()
    {
    }

    public void GameStart()
    {
        InitCellList();
    }

    public void BroadcastCardEvent(AbstractCardEvent cardEvent)
    {

    }
}