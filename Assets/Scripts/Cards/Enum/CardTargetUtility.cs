using System;

public static class CardTargetUtility
{
    /// <summary>
    /// 判断卡牌是否可以作为相应卡牌目标
    /// </summary>
    public static bool CardIsTarget(ISeletableTarget target, CardTarget cardTarget)
    {
        Func<CardTarget, bool> IsTarget = (t) => IsTargetsCompatible(cardTarget, t);
        var card = (target as CardVisual)?.card;
        if (card != null)
        {
            if (card.field != null && card.field.state != BattleState.Survive) return false;
            return (IsTarget(CardTarget.Monster) && card.type == CardType.Monster && card.field.cell!=null) ||
                    (IsTarget(CardTarget.Enemy) && card.type == CardType.Enemy) ||
                    (IsTarget(CardTarget.Player) && card.type == CardType.Player) ||
                    (IsTarget(CardTarget.FriendlyDerive) && card.type == CardType.FriendlyDerive) ||
                    (IsTarget(CardTarget.EnemyDerive) || card.type == CardType.EnemyDerive);
        }
        else if (IsTarget(CardTarget.Cell))
            return target is Cell;
        return false;
    }
    public static bool IsTargetsCompatible(CardTarget target1, CardTarget target2)
    {
        return (target1 & target2) != 0;
    }

}
