using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CanRepeat(false)]
public class ActionComponent : CardComponent, ISelector
{
    public List<ISeletableTarget> Targets { get; } = new List<ISeletableTarget>();
    int cost => card.ContainsTag("迅捷") ? 0 : 1;
    public int TargetCount => 1;
    int actionTimes = 1;
    public List<CardTarget> CardTargets { get; } = new List<CardTarget>() { CardTarget.None };
    public void CancleSelect()
    {
        Targets.Clear();
    }
    public override void TurnReset()
    {
        actionTimes = 1;
    }

    public bool CanSelectTarget(ISeletableTarget target, int i)
    {
        // if (target == card.visual) return false;
        // 先判定攻击
        if (card.attack != null
                && CardTargetUtility.IsTargetsCompatible(CardTargets[i], CardTarget.Enemy | CardTarget.EnemyDerive)
                && target is EnemyVisual)
        {
            var e = ((EnemyVisual)target).card;
            var dis = e.attacked.GetAttackDistance(card);
            //通过优先级来计算
            Debug.Log($"CanSelectTarget: {target}, {e.attacked.GetAttackDistance(card) <= card.attack.AtkRange}, {e.attacked.GetAttackedPriority(card) == targetPriority}");
            return e.attacked.GetAttackDistance(card) <= card.attack.AtkRange && e.attacked.GetAttackedPriority(card) == targetPriority;
        }

        // 再判定移动
        if (card.field != null)
        {
            if (CardTargetUtility.IsTargetsCompatible(CardTargets[i], CardTarget.Cell) && target is Cell c)
            {
                var dis = CellManager.Instance.GetStreetDistance(card.field.cell, c);
                return dis <= card.field.moveRange && dis > 0;
            }

            var visual = (CardVisual)target;
            if ((CardTargetUtility.IsTargetsCompatible(CardTargets[i], CardTarget.EnemyDerive)
                        && visual.card.type == CardType.EnemyDerive)
                    || CardTargetUtility.IsTargetsCompatible(CardTargets[i], CardTarget.Monster)
                    )
            {
                var cell = visual.card.field.cell;
                var dis = CellManager.Instance.GetStreetDistance(card.field.cell, cell);
                return (cell.CanMove() || cell.CanSwaped()) && dis <= card.field.moveRange && dis > 0;
            }
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
                var tauntEnemies = CardManager.Instance.Enemies.FindAll(e => e.field.state == BattleState.Survive && e.attacked.Taunt);
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
                    var enemies = CardManager.Instance.Enemies.FindAll(e => e.field.state == BattleState.Survive && e.attacked.GetAttackDistance(card) <= card.attack.AtkRange);
                    if (enemies.Count > 0)
                    {
                        targetPriority = enemies[0].attacked.GetAttackedPriority(card);
                        return true;
                    }
                }
            }
            return false;
        }
    }

    private bool Taunted
        => CardManager.Instance.Enemies.FindAll(e => e.field.state == BattleState.Survive && e.attacked.Taunt).Count > 0;



    public bool CanUse()
    {
        if (card.camp == CardCamp.Enemy) return false;
        if (actionTimes <= 0) { ShowUsedMessage(); return false; }
        if (GameManager.Instance.Pp < cost) { ShowPPMessage(); return false; }
        canAttack = CanAttack;
        canMove = CanMove;
        return canAttack || canMove;
    }

    public void Excute()
    {
        var target = Targets[0];
        if (target is CardVisual visual)
        {
            if (card.attack != null && visual.card.camp != CardCamp.Friendly)
                card.attack.Attack(visual.card, true, cost);
            else card.field.Move(visual.card.field.cell, true, cost);
        }
        else if (target is Cell cell)
            card.field.Move(cell, true, cost);
        card.ClearTag("迅捷");
        actionTimes -= 1;
    }

    public ISelector GetNextSelector()
    {
        return null;
    }

    public void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
        Targets.Clear();
        CardTargets[0] = CardTarget.None;
        if (CanMove) CardTargets[0] |= CardTarget.Cell | CardTarget.EnemyDerive | CardTarget.Monster;
        if (CanAttack) CardTargets[0] |= CardTarget.Enemy | CardTarget.EnemyDerive;
        if (Taunted) ShowTauntMessage();
    }

    public bool GetCanAttack()
    {
        return CanAttack;
    }


    private void ShowTauntMessage()
    {
        Notice.Instance.setNotice("你必须攻击那个具有嘲讽的敌人。");
        //Debug.Log("这个随从被嘲讽了，只能攻击带有嘲讽的敌人！");
    }

    private void ShowUsedMessage()
    {
        Notice.Instance.setNotice("这个随从已经行动过了。");
        //Debug.Log("这个随从已经行动过了");
    }

    private void ShowPPMessage()
    {
        Notice.Instance.setNotice("没有足够的PP！");
        //Debug.Log("没有足够的PP！");
    }
}
