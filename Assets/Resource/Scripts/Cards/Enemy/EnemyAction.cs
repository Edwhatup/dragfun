using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyAction
{
    public int turn;
    public Action action;
    public EnemyActionType type;
    public EnemyAction(int turn,Action action,EnemyActionType type)
    {
        this.turn = turn;
        this.action = action;
        this.type = type;
    }
}
public enum EnemyActionType
{
    Once,   
    Loop
}
