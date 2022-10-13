using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    public List<ISeletable> selections = new List<ISeletable>();
    List<ISeletable> allCanSelections = new List<ISeletable>();

    public static Selections Instance = new Selections();
    public bool HasSelectObject => selections.Count > 0;
    public ISeletable Selection => selections.Count > 0 ? selections[0] : null;
    private Selections()
    {

    }
    public void AddCanSelection(ISeletable seletable)
    {
        if (!allCanSelections.Contains(seletable))
        {
            allCanSelections.Add(seletable);
        }
    }
    public void RemoveCanSelection(ISeletable seletable)
    {
        if(allCanSelections.Contains(seletable))
            allCanSelections.Remove(seletable);
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            selections.Clear();
            UpdateAllSelectableVisual();
        }
    }


    public void Click(ISeletable seletable)
    {
        if (Selection != null && Selection.TargetCount == selections.Count - 1)
        {
            
        }
        UpdateAllSelectableVisual();
    }

    public void UpdateAllSelectableVisual()
    {
        foreach (var selection in allCanSelections)
        {
            selection.UpdateSelectableVisual();
        }
    }
}
