using System;
using System.Collections.Generic;

public class ShopSingleBuffEffect : ShopBuffEffect
{
    public ShopSingleBuffEffect(CardBuff buff) : base(buff) { }

    public override bool AllowRepeat => false;

    public override bool IncludeSelf => false;

    public override List<Func<Card, bool>> GetSelectTargets() => new List<Func<Card, bool>>() { ShopSelections.All };
}