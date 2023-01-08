using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    enum State
    {
        UseCard,
        ControlMonster,
    }
    State state;
    public static Selections Instance { get; private set; }

    [SerializeField]
    GameObject arrowPrefab;
    public Transform selectEleParent;

    GameObject arrow;
    [HideInInspector]
    public GameObject mouseFollowObject;


    List<ISeletableTarget> allTargets = new List<ISeletableTarget>();
    public List<ISelector> selectors = new List<ISelector>();
    public List<ISeletableTarget> Targets => Selector.Targets;
    public bool FinishSelect => Selector != null && Selector.Targets.Count >= Selector.TargetCount;

    public Card source;
    public ISelector Selector => selectors.Back();
    #region ISelectable 注册相关方法
    public void AddCanSelection(ISeletableTarget seletable)
    {
        if (seletable == null) return;
        if (!allTargets.Contains(seletable))
        {
            allTargets.Add(seletable);
            seletable.UpdateSelectableVisual(CanSelect(seletable));
        }
    }


    public void RemoveCanSelection(ISeletableTarget seletable)
    {
        if (allTargets.Contains(seletable))
            allTargets.Remove(seletable);
    }
    #endregion
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Update()
    {
        if (mouseFollowObject)
        {
            Vector2 vector2 = Input.mousePosition;
            mouseFollowObject.transform.position = vector2;
        }
        if (Input.GetMouseButton(1))
        {
            Clear();
        }
    }
    public void Clear(bool isCancled = true)
    {
        DestoryArrow();
        mouseFollowObject = null;
        if(isCancled)
        {
            foreach (var s in selectors)
                s.CancleSelect();
        }
        selectors.Clear();
        UpdateAllSelectableVisual();
        GameManager.Instance.Refresh();
    }

    public bool TryAddSelector(ISelector selector)
    {
        if (selector == null) return false;
        //Debug.Log(selector.GetType().Name); 
        if (selectors.Count == 0 && selector.CanUse())
        {
            selectors.Add(selector);
            selector.OnSelected();
            UpdateAllSelectableVisual();
            GameManager.Instance.Refresh();
            return true;
        }
        return false;
    }
    public void TryAddSelectTarget(ISeletableTarget target)
    {
        if (!CanSelect(target)) return;
        //Debug.Log(target.GetType().Name);
        Targets.Add(target);
        if (FinishSelect)
        {
            var s = Selector.GetNextSelector();
            if (s != null && s.CanUse())
            {
                selectors.Add(s);
                DestoryArrow();
                mouseFollowObject = null;
                s.OnSelected();
            }
            else
            {
                foreach (var item in selectors)
                    item.Excute();
                Clear(false);
                GameManager.Instance.Refresh();
            }
        }
        UpdateAllSelectableVisual();
    }
    //判断当前目标是否可以使target
    private bool CanSelect(ISeletableTarget target)
    {
        if (target == null) return false;
        if (Selector == null || Selector==target || Targets.Contains(target)) return false; 
        int i = Targets.Count;
        return i < Selector.TargetCount &&
                CardTargetUtility.CardIsTarget(target, Selector.CardTargets[i]) &&
                Selector.CanSelectTarget(target, i);
    }
    public void CreateArrow(Transform startTrans)
    {
        arrow = GameObject.Instantiate(arrowPrefab, startTrans.position, Quaternion.identity, selectEleParent);
    }
    public void DestoryArrow()
    {
        Destroy(arrow);
    }
    public void UpdateAllSelectableVisual()
    {
        foreach (var selection in allTargets)
        {
            selection.UpdateSelectableVisual(CanSelect(selection));
        }
    }
}