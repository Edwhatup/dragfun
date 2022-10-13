using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeletable
{
    bool CanSelect();
    void UpdateSelectableVisual();
    int TargetCount { get; }

}
