using System;
using System.Collections.Generic;

public abstract class ShopEffect
{
    public abstract bool AllowRepeat { get; }
    public abstract bool IncludeSelf { get; }
    public abstract List<Func<Card, bool>> GetSelectTargets();
    public abstract void Execute(List<Card> cards);
}

public abstract class ShopBuffEffect : ShopEffect
{
    private CardBuff buff;

    protected ShopBuffEffect(CardBuff buff)
    {
        buff.LifeType = BuffLifeType.Permanent;
        this.buff = buff;
    }

    public override void Execute(List<Card> cards)
    {
        cards.ForEach(i => i.AddBuff(buff));
    }
}