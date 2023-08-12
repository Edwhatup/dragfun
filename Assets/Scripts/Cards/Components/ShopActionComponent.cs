using System;

public class ShopActionComponent : CardComponent
{
    public ShopEffect BuyEffect => buyEffect;
    private ShopEffect buyEffect;

    public ShopActionComponent(ShopEffect buyEffect)
    {
        this.buyEffect = buyEffect;
    }
}

public static class ShopSelections
{
    public static readonly Func<Card, bool> All = c => true;
    public static readonly Func<Card, bool> Derive = c => c.type == CardType.FriendlyDerive;
    public static readonly Func<Card, bool> Spell = c => c.type == CardType.Spell;
}