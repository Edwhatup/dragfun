public class AddArmorCellEffect : ConstantCellEffect
{
    // -1是永久
    protected override int LifeTime => -1;

    private int amount = 1;

    public AddArmorCellEffect(Card card) : base(card) { }

    public override void Execute(Card c)
    {
        c.attacked.block += amount;
    }

    public override void Undo(Card c)
    {
        c.attacked.block -= amount;
    }

    public override string ToString()
    {
        return $"为周围的随从增加 {amount} 点护甲值";
    }

    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.Monster);
    }
}