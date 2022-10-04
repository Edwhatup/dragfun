using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy
{
    public int id;
    public string enemyName;
    public Enemy(int _id, string _enemyName)
    {
        this.id = _id;
        this.enemyName = _enemyName;
    }
}

public class NormalEnemy: Enemy
{
    public int enemyHP;
    public int enemyHPMax;

    public NormalEnemy(int _id,string _enemyName,int _enemyHPMax): base(_id,_enemyName)
    {
        this.enemyHP=_enemyHPMax;
        this.enemyHPMax=_enemyHPMax;
    }
}

public class BossEnemy: Enemy
{
    public int enemyHP;
    public int enemyHPMax;

    public BossEnemy(int _id,string _enemyName,int _enemyHPMax): base(_id,_enemyName)
    {
        this.enemyHP=_enemyHPMax;
        this.enemyHPMax=_enemyHPMax;
    }
}
