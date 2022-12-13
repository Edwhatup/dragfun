using System.Collections.Generic;

public class FieldComponnet : CardComponent
{
    public Cell cell;
    public BattleState state;
    Ref<int> moveRange;
    Ref<int> canMove;
    Ref<int> canSwap;
    Ref<int> moveCost;
    Ref<int> moveFree;
    public FieldComponnet()
    {
        this.canMove = new Ref<int>(1);
        this.canSwap = new Ref<int>(1);
        this.moveCost = new Ref<int>(1);
        this.moveRange = new Ref<int>(1);
        this.moveFree = new Ref<int>(0);
    }
    public int MoveCost => moveFree.value <= 0 && moveCost.value > 0 ? 1 : 0;
    public bool CanMove => canMove.value > 0;
    public int MoveRange => moveRange.value > 0 ? moveRange.value : 0;
    public bool CanSwap => canSwap.value > 0;

    public override void Add(CardComponent component)
    {
        throw new System.Exception("不能重复添加场地组件");
    }

    public override string Desc()
    {
        switch (state)
        {
            case BattleState.Survive:
                return "存活";
            case BattleState.Dead:
                return "死亡";
            case BattleState.HalfDead:
                return "濒死";
            default: throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 是当前卡牌移动到指定单元格
    /// </summary>
    /// <param name="targetCell">目标场地</param>
    /// <param name="active">是否主动发起移动</param>
    public void Move(Cell targetCell, bool active)
    {
        if (!targetCell.CanMove()) return;
        int cost = active ? MoveCost : 0;

        var e = new MoveEvent(card, targetCell.card, cell, targetCell, cost);
        GameManager.Instance.BroadcastCardEvent(e);

        if (targetCell.card != null)
        {
            var monster = targetCell.card;
            cell.Summon(monster);
        }
        targetCell.Summon(card);
        GameManager.Instance.BroadcastCardEvent(e.EventAfter());
    }
}
