using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card.Enemy {
    public static class EnemyActions
    {
        public static void ApplyDamage2Player(EnemyCard source, int damage)
        {
            Debug.Log("造成伤害");
            Player.Instance.ApplyDamage(damage);
        }
    }

}

