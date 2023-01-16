using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Attack,
    Other
}
public class DamageInfo
{
    public bool isResist;
    public int initDamage;
    public int actualDamage;
    public BattleState beforeState;
    public BattleState afterState;
    public bool IsLethalDamage => beforeState == BattleState.Survive && (afterState == BattleState.Dead || afterState==BattleState.HalfDead);
    public DamageInfo(bool isResist,  int initDamage, int actualDamage)
    {
        this.isResist = isResist;
        this.initDamage = initDamage;
        this.actualDamage = actualDamage;
    }
    public DamageInfo() { }
}
