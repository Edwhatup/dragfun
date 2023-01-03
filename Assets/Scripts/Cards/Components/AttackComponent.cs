using System.Linq;
using System.Collections.Generic;
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

    bool Sweep => sweep > 0;
    bool Pierce=>pierce > 0;
    public bool CanAttack => canAttack>0;
    public int AtkRange => atkRange > 0 ? atkRange : 0;
    public AttackComponent(int atk)
    {
        this.atk = atk;
    }
    public override void Init()
    {        
        this.atk = initAtk;
    }

    public void Attack(Card target, bool active=true,int cost=0)
    {
        if (target.attacked == null || !CanAttack) return;
        int ppcost = active ? cost : 0; 
        var e = new BeforeAttackEvent(card, target);
        GameManager.Instance.BroadcastCardEvent(e);
        var info= target.GetComponent<AttackedComponent>().ApplyDamage(card,atk);
        if(!info.isResist)
        {
            List<Card> targets = new List<Card>();
            if (Sweep)
            {
                var sameRowTarget = CardManager.Instance.cards
                                  .FindAll(c => c.camp != card.camp && c.field.row != null && c.field.row == target.field.row)
                                   .ToList();
                targets.AddRange(sameRowTarget);
            }
            if (Pierce)
            {
                var sameColTarget = CardManager.Instance.cards
                                  .FindAll(c => c.camp != card.camp && c.field.row != null && c.field.col == target.field.col)
                                   .ToList();
                targets.AddRange(sameColTarget);
            }
            foreach (var t in targets)
            {
                t.GetComponent<AttackedComponent>().ApplyDamage(card, atk);
            }
        }
        var ae = new AfterAttackEvent(card, target, ppcost, info);
        GameManager.Instance.BroadcastCardEvent(ae);
    }
    public override string ToString()
    {
        if (atk > initAtk) return $"<color=green>{atk}</color>";
        else return $"<color=white>{atk}</color>";
    }
}
