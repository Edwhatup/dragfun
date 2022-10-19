using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Seletion
{
    public interface ISeletable
    {
        void UpdateSelectableVisual();
        void Start();
        void OnDestroy();
    }
    public interface ISeletableSource : ISeletable
    {
        //判断当前目标是否可以作为被选中的目标
        bool JudgeCanSelect(ISeletable seletable);
        int TargetCount { get; }
        bool CanSelect();
    }
}