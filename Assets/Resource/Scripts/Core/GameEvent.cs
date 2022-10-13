using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.GameEvent
{
    public interface IOnMove
    {
        void OnMove();
    }
    public interface IOnAttack
    {
        void OnAttack();
    }
    public interface IOnSummon
    {
        void OnSunmon();
    }
    public interface IOnDead
    {
        void OnDead();
    }
}


