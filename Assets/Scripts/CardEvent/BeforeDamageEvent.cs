/// <summary>
/// 造成伤害的事件
/// </summary>
public class BeforeDamageEvent : AbstractCardEvent
{
    public int damage;
    public BeforeDamageEvent(Card source, Card target, int damage) : base(source)
    {
        this.damage = damage;
    }
}
public class AfterDamageEvent : AbstractCardEvent
{
    public DamageInfo info;
    public AfterDamageEvent(Card source, Card target,DamageInfo info) : base(source)
    {
        this.info = info;
    }
}
