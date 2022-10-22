using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public interface IManager
    {
        void Refresh();
        void GameStart();
        void PlayerAction();
        void EnemyAction();
    }
}