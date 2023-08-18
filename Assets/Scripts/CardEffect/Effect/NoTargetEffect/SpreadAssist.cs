public class SpreadAssist : PassiveCardEffect
{
    private int atk, hp, ed;

    public SpreadAssist(Card card, int atk, int hp, int extraDamage) : base(card)
    {
        this.atk = atk;
        this.hp = hp;
        ed = extraDamage;
    }

    public override void HandleEvent(AbstractCardEvent e)
    {
        if (e is AfterUseEvent u)
        {
            u.source.AddComponent(new PassiveEffectComponent(new SpreadAssist(u.source, atk, hp, ed)));
            u.source.AddBuff(new StatsPositiveBuff(atk, hp, ed));
        }
    }
}