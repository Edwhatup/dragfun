public class BeforeAttackEvent : AbstractCardEvent
{
    public BeforeAttackEvent(Card source, Card target) : base(source, target, 0)
    {

    }
}
public class AfterAttackEvent : AbstractCardEvent
{

    public DamageInfo damageInfo;
    public AfterAttackEvent(Card source, Card target, int ppCost,DamageInfo info) : base(source, target, ppCost)
    {
        this.damageInfo = info;
    }
}