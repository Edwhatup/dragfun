using Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Seletion
{
    public interface ISeletable
    {
        void UpdateSelectableVisual();
    }
    public interface ISeletableSource
    {
        int TargetCount { get; }
        CardTarget[] CardTargets { get; }
        bool CanSelect(AbstractCard card, int i);
    }
}