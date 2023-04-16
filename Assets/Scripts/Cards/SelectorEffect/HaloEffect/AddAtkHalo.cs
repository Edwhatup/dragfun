using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAtkHalo : HaloEffect
{
    private int amount;

    // 看情况而定，如果你只想让这个光环作用在绝对的坐标，那就重写这一个构造函数
    // 相对的坐标同理
    // 但如果两个都要，就两个都重写
    public AddAtkHalo(Card card, List<Cell> cells, int amount) : base(card, cells)
    {
        this.amount = amount;
    }

    // 光环用相对坐标，则重写这个！
    public AddAtkHalo(Card card, List<Vector2Int> positions, int amount) : base(card, positions)
    {
        this.amount = amount;
    }

    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.Monster);
    }

    // 添加光环时，就加数值
    public override void Execute(Card c)
    {
        c.attack.atk += amount;
    }

    // 撤销光环时，就把加的数值减回来
    // 理论上，HaloComponent会把Execute和Undo调度得很好，不会出现多加一次或者多减一次的情况
    // 如果不幸真的出现了再修吧 QAQ
    public override void Undo(Card c)
    {
        c.attack.atk -= amount;
    }

    public override string ToString()
    {
        return $"为周围的随从增加 {amount} 点护甲值";
    }
}
