using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
using Card.Enemy;
using Card.Monster;
using Card.Spell;
using System;

public static class CardTargetUtility
{
    /// <summary>
    /// 判断卡牌是否可以作为相应卡牌目标
    /// </summary>
    public static bool CardIsTarget(AbstractCard card, CardTarget cardTarget)
    {
        Func<CardTarget,bool> IsTarget=(t)=>IsTargetsCompatible(cardTarget,t);
        if (card is MonsterCard)
        {
            var monster = card as MonsterCard;
            switch(monster.state)
            {
                case PlayerCardState.OnBoard:
                    return IsTargetsCompatible(cardTarget,)
            }

        }
    }
    public static bool IsTargetsCompatible(CardTarget target1, CardTarget target2)
    {
        return (target1 | target2) != 0;
    }

}
