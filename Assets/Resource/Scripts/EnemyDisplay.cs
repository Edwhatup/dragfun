using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyDisplay : MonoBehaviour,IPointerDownHandler
{
    public Text nameText;
    public Text healthText;
    public Enemy enemy;

    public GameObject selectedEdge;
    // Start is called before the first frame update
    void Start()
    {
        showEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showEnemy()
    {
        nameText.text = enemy.enemyName;
        if(enemy is BossEnemy)
        {
            var boss = enemy as BossEnemy;
            healthText.text = (boss.enemyHP.ToString()+"/"+boss.enemyHPMax.ToString());
        }

        if(enemy is NormalEnemy)
        {
            var normal = enemy as NormalEnemy;
            healthText.text = (normal.enemyHP.ToString()+"/"+normal.enemyHPMax.ToString());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(BattleManager.Instance.GetWaitingCardState() == 2)
        {
            BattleManager.Instance.AttackConfirm(gameObject);//还没写完attack 
            //Debug.Log("onboard");
            
        }    
    }
}
