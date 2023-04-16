using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAroundAtkHalo : HaloEffect
{
    protected override List<Cell> Cells 
        => CellManager.Instance.GetCells().FindAll(c => CellManager.Instance.GetStreetDistance(c, this.card.field.cell) == 1);
    private int atkValue;

    public AddAroundAtkHalo(Card c, int atkValue) : base(c)
    {
        this.atkValue = atkValue;
    }

    public override void InitTarget()
    {
        CardTargets.Add(CardTarget.Monster);
    }

    // 添加光环时，就加数值
    public override void Execute(Card c)
    {
        c.attack.atk += atkValue;
    }

    // 撤销光环时，就把加的数值减回来
    // 理论上，HaloComponent会把Execute和Undo调度得很好，不会出现多加一次或者多减一次的情况
    // 如果不幸真的出现了再修吧 QAQ
    public override void Undo(Card c)
    {
        c.attack.atk -= atkValue;
    }

    public override string ToString()
    {
        return $"为周围四格的随从增加 {atkValue} 点攻击力";
    }
}
