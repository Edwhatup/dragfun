using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

[CanRepeat(false)]
public class HaloComponent : EventListenerComponent
{
    protected HaloEffect Buff { get => effect as HaloEffect; }

    private bool isCardPlaced = false;

    public HaloComponent(HaloEffect buff) : base(buff) { }

    public override void EventListen(AbstractCardEvent e)
    {
        // 如果既不是自己，又不是目标，或者目标不存在, 则不处理
        if (e.source == null || (e.source != base.card && !IsTarget(e.source))) return;
        // Log("触发了光环检测");

        switch (e)
        {
            case AfterSummonEvent u:
                // 是自己，则给周围加光环
                if (u.source == base.card)
                {
                    if (isCardPlaced) return;
                    isCardPlaced = true;
                    // Log("光环所有者被放置, 给周围的目标加Buff");
                    GetAvailbleTargets().ForEach(c =>
                    {
                        Buff.Execute(c);
                        Log($"给 {c.name} 加了光环");
                    });
                }

                // 是范围内的目标（非目标在开头除掉了），加光环
                else if (IsInRange(u.source))
                {
                    Buff.Execute(u.source);
                    // Log($"在光环范围内有目标被放置\n给 {u.source.name} 加了光环");
                }
                break;

            // 自己移动前，撤销所有的光环，等移动后再加回来
            case BeforeMoveEvent be:
                if (be.source == base.card)
                    GetAvailbleTargets().ForEach(c => Buff.Undo(c));
                break;

            case AfterMoveEvent m:
                var card = m.source;

                // 自己移动后，加回来
                if (card == base.card)
                {
                    GetAvailbleTargets().ForEach(c => Buff.Execute(c));
                    return;
                }

                // 别的卡进入，加光环
                if (IsEntering(m)) Buff.Execute(card);

                // 别的卡退出，去光环
                else if (IsExiting(m)) Buff.Undo(card);
                break;

            case DeathEvent d:
                // 自己死亡，取消所有光环
                if (d.source == base.card)
                    GetAvailbleTargets().ForEach(c => Buff.Undo(c));

                // 别的卡死亡, 去光环
                else if (IsTarget(d.source)) Buff.Undo(d.source);
                break;
        }

    }

    private List<Card> GetAvailbleTargets() => CardManager.Instance.board.FindAll(c => IsInRange(c) && IsTarget(c));

    private bool IsTarget(Card c) => CardTargetUtility.CardIsTarget(c, Buff.CardTargets);

    private bool IsInRange(Card c) => Buff.IsInRange(c.field.cell);

    private bool IsEntering(AfterMoveEvent move) => Buff.IsInRange(move.targetCell) && !Buff.IsInRange(move.sourceCell);
    private bool IsExiting(AfterMoveEvent move) => Buff.IsInRange(move.sourceCell) && !Buff.IsInRange(move.targetCell);


    public override string ToString() => $"光环: {Buff.ToString()}";
}
