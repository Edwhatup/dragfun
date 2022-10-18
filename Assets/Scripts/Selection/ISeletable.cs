using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Seletion
{
    public interface ISeletable
    {
        bool CanSelect();
        void UpdateSelectableVisual();
        void Start();
        void OnDestroy();
    }
    public interface ISeletableSource : ISeletable
    {
        int TargetCount { get; }
        CardTarget CurrentTarget { get; }
    }
}