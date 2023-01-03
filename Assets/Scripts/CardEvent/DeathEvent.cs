//死亡事件
public class DeathEvent : AbstractCardEvent
{
    public DeathEvent(Card killer, Card dead) : base(killer, dead)
    {

    }
}