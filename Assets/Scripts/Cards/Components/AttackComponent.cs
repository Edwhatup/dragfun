using System.Linq;
using System.Collections.Generic;
using UnityEngine;
[CanRepeat(false)]
[RequireCardComponent(typeof(FieldComponnet))]
public class AttackComponent : CardComponent
{
    public int atkRange=1;
    public int atk=0;
    public int initAtk=0;
    public int canAttack=1;
    public int sweep=0;
    public int pierce=0;
    public int extraDamage=0;
    public int extraDamageRate=1;
    public int buffAtkByRange=0;
    public int globalAtkCount=0;

    bool Sweep => sweep > 0;
    bool Pierce=>pierce > 0;
    bool BuffAtkByRange=> buffAtkByRange > 0;
    public bool CanAttack => canAttack>0;
    public int AtkRange => atkRange > 0 ? atkRange : 0;
    public AttackComponent(int atk)
    {
        this.initAtk = atk;
    }

    //log里面输出atk
    public void DebugShowAtk(Card card)
    {
        Debug.Log(card.name+card.GetComponent<AttackComponent>().atk);
    }
    public override void Init()
    {        
        this.atk = initAtk;
    }

    public void RangeUp(Card target,int extraRange)
    {
        this.atkRange+=extraRange;
        //Debug.Log(this.atkRange);
    }

    public void Attack(Card target, bool active=true,int cost=0)
    {
        if (target.attacked == null || !CanAttack) return;
        int ppcost = active ? cost : 0; 
        var e = new BeforeAttackEvent(card, target);
        GameManager.Instance.BroadcastCardEvent(e);

        if(BuffAtkByRange) extraDamage+=AtkRange;

        var info= target.GetComponent<AttackedComponent>().ApplyDamage(card,atk,DamageType.Attack);
        if(!info.isResist)
        {
            List<Card> targets = new List<Card>();
            if (Sweep)
            {
                var sameRowTarget = CardManager.Instance.GetSameRowEnemyUnits(card,target);
                targets.AddRange(sameRowTarget);
            }
            if (Pierce)
            {
                var sameColTarget = CardManager.Instance.GetSameColEnemyUnits(card,target);
                targets.AddRange(sameColTarget);
            }
            targets=targets.Distinct().ToList();
            targets.Remove(target);
            foreach (var t in targets)
            {
                t.GetComponent<AttackedComponent>().ApplyDamage(card, atk,DamageType.Attack);
            }
        }
        var ae = new AfterAttackEvent(card, target, ppcost, info);
        globalAtkCount+=1;
        GameManager.Instance.BroadcastCardEvent(ae);
    }
    public override string ToString()
    {
        if (atk > initAtk) return $"<color=green>{atk}</color>";
        else return $"<color=white>{atk}</color>";
    }

}
