/// <summary>
/// 造成伤害的事件
/// </summary>
public class DamageEvent : AbstractCardEvent
{
    public int damage;

    public DamageEvent(Card source, Card target, int damage) : base(source)
    {
        this.damage = damage;
    }
    /// <summary>
    /// 是否是致死伤害
    /// </summary>
    public bool IsLethal { get; set; }
}