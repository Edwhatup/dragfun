using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackTarget : MonoBehaviour ,IPointerClickHandler
{
    public bool attackable = false;
    CardDisplay display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(attackable)
        {
            //BattleManager.Instance.AttackConfirm(gameObject);
        }
    }

    public void ApplyDamage(int _damage)
    {
        //Enemy enemy = GetComponent<EnemyDisplay>().enemy;
        //if(enemy is BossEnemy)
        //{
        //    BossEnemy bossEnemy = GetComponent<EnemyDisplay>().enemy as BossEnemy;
        //    bossEnemy.enemyHP -=_damage;
        //    if(bossEnemy.enemyHP<=0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
        //if(enemy is NormalEnemy)
        //{
        //    NormalEnemy normalEnemy = GetComponent<EnemyDisplay>().enemy as NormalEnemy;
        //    normalEnemy.enemyHP -=_damage;
        //    if(normalEnemy.enemyHP<=0)
        //    {
        //        Destroy(gameObject);
        //    }
        //}
    }
}
