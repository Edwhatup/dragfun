﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionComponent : CardComponent, ISelector
{
    List<ISeletableTarget> targets = new List<ISeletableTarget>();
    public List<ISeletableTarget> Targets => targets;
    int cost => card.ContainsTag("迅捷") ? 0 : 1;
    public int TargetCount => 1;
    int actionTimes = 1;
    List<CardTarget> cardTargets=new List<CardTarget>();
    public IReadOnlyList<CardTarget> CardTargets => cardTargets;
    public void CancleSelect()
    {
        return;
    }

    public bool CanSelectTarget(ISeletableTarget target, int i)
    {
        if (CardTargetUtility.IsTargetsCompatible(CardTargets[0], CardTarget.Cell) && target is Cell cell)
        {
            return CellManager.Instance.GetStreetDistance(card.field.cell, cell) <=card.field.moveRange;
        }
        if (CardTargetUtility.IsTargetsCompatible(CardTargets[0], CardTarget.Enemy) && target is EnemyVisual visual)
        {
            var e=target as Card;
            //通过优先级来计算
            return e.attacked.GetAttackDistance(card) <= card.attack.AtkRange && e.attacked.GetAttackedPriority(card) == targetPriority;

        }
        return false;
    }
    int targetPriority;
    bool canMove;
    bool canAttack;
    bool CanMove
    {
        get
        {
            if (card.field?.CanMove ?? false)
                return CellManager.Instance.GetAllSpecifyCells((c) =>
                (c.CanMove() || c.CanSwaped()) &&
                CellManager.Instance.GetStreetDistance(card.field.cell, c) <= card.field.moveRange)
                .Count > 0;
            return false;
        }
    }
    bool CanAttack
    {
        get
        {
            if (card.attack?.CanAttack ?? false)
            {
                var tauntEnemies = CardManager.Instance.enemies.FindAll(e => e.attacked.Taunt);
                if (tauntEnemies.Count > 0)
                {
                    foreach (var taunt in tauntEnemies)
                    {
                        if (taunt.attacked.GetAttackDistance(card) <= card.attack.AtkRange)
                        {
                            targetPriority = taunt.attacked.GetAttackedPriority(card);
                            return true;
                        }
                    }
                }
                else
                {
                    var enemies = CardManager.Instance.enemies.FindAll(e => e.attacked.GetAttackDistance(card) <= card.attack.AtkRange);
                    if(enemies.Count>0)
                    {
                        targetPriority = enemies[0].attacked.GetAttackedPriority(card) ;
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public bool CanUse()
    {
        if (actionTimes <= 0 && GameManager.Instance.pp < cost) return false;
        canAttack = CanAttack;
        canMove = CanMove;
        return canAttack || canMove;
    }

    public void Excute()
    {
        var target = targets[0];
        if (target is EnemyVisual visual)
            card.attack.Attack(visual.card,true, cost);
        else if (target is Cell cell)
            card.field.Move(cell,true, cost);
        card.ClearTag("迅捷");
    }

    public ISelector GetNextSelector()
    {
        return null;
    }

    public void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
        cardTargets.Clear();
        if(canMove) cardTargets.Add(CardTarget.Cell);
        if(canAttack) cardTargets.Add(CardTarget.Enemy);

    }
}