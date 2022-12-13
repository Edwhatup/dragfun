using System.Collections.Generic;
/// <summary>
/// 该Selector可以选择Cell为目标进行交换和移动，选择敌人为目标进行攻击
/// </summary>
public class MonsterFieldSelector : CardSelector
{

    public MonsterFieldSelector(Card card)
    {
        this.card = card;
        targetCount = 1;
        cardTargets = new List<CardTarget>();
        var target = CardTarget.None;
        if (card.field?.CanMove ?? false)
            target = target | CardTarget.Cell;
        if (card.attack?.CanAttack ?? false)
            target = target | CardTarget.Enemy;
        cardTargets.Add(target);
    }

    public override bool CanUse()
    {
        if (card.field?.CanMove ?? false &&
            GameManager.Instance.pp>=card.field.MoveCost &&
            CellManager.Instance.GetAllSpecifyCells((c) =>
            CellManager.Instance.GetStreetDistance(card.field.cell, c) <= card.field.MoveRange)
            .Count > 0)
            return true;
        if (card.attack?.CanAttack ?? false &&
            GameManager.Instance.pp >= card.attack.AtkCost &&
            card.field.cell.row < card.attack.AtkRange &&
            EnemyManager.Instance.GetAllEnemies().Count > 0)
            return true;
        return false;
    }
    public override bool CanSelectTarget(ISeletableTarget target, int i)
    {
        if (!base.CanSelectTarget(target, i)) return false;
        if(CardTargetUtility.IsTargetsCompatible(CardTargets[0],CardTarget.Cell) &&  target is Cell cell)
        {
            return CellManager.Instance.GetStreetDistance(card.field.cell, cell) == 1;
        }
        if(CardTargetUtility.IsTargetsCompatible(CardTargets[0], CardTarget.Enemy) &&  target is EnemyVisual visual)
        {
            return card.field.cell.row<card.attack.AtkRange;
        }
        return false;   

    }
    public override void Excute()
    {
        var target = targets[0];
        if (target is EnemyVisual visual)
            card.attack.Attack(visual.card, true);
        else if (target is Cell cell)
            card.field.Move(cell, true);
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }
}
