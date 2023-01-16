using System.Collections.Generic;
public interface ISeletableTarget
{
    void UpdateSelectableVisual(bool canSelect);
}
public interface ISelector
{
    List<ISeletableTarget> Targets { get; }
    int TargetCount { get; }
    List<CardTarget> CardTargets { get; }
    bool CanSelectTarget(ISeletableTarget target, int i);
    bool CanUse();
    void Excute();
    ISelector GetNextSelector();
    void CancleSelect();
    void OnSelected();
}
