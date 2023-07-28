using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HaloEffect : CellEffect
{
    public HaloEffect(Card card) : base(card) { }

    // 不使用这个玩意了，封掉！
    public sealed override void Excute() { }
}
