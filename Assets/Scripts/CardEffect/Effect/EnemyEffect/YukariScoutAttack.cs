using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukariScoutAttack : NoTargetCardEffect
{
    int damage; 
    public YukariScoutAttack(Card card,int damage) : base(card)
    {
        this.damage = damage;
    }
    public override string ToString()
    {
        return $"随机对同行或者同列的一个你的单位造成{damage}点伤害，然后随机移动一格。";
    }
    public override void Excute()
    {
        var targets=CellManager.Instance.GetCells().FindAll(c=> c.card!=null && c.card.camp==CardCamp.Friendly && c.card.attacked!=null && (c.card.field.row==this.card.field.row || c.card.field.col==this.card.field.col));
        if (targets.Count > 0)
            targets[0].card.attacked.ApplyDamage(card, damage);
        else 
            Player.Instance.attacked.ApplyDamage(card, damage);
        int offsetX = Random.Range(-1, 2);
        int offsetY = Random.Range(-1, 2);
        Cell targetCell = CellManager.Instance.GetSpecifyCell(c=>c.row==card.field.row+offsetX && c.col==card.field.col+offsetY);
        this.card.field.Move(targetCell,true);
    }
}