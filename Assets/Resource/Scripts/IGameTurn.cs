using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameTurn
{
    void GameStart();
    void PlayerAction();
    void EnemyAction();
    void PlayDraw();
}
