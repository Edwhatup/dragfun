[CanRepeat(false)]
public class EnemyRuneComponent : EventListenerComponent
{
    private EnemyRune rune;

    public EnemyRuneComponent(EnemyRune rune)
    {
        this.rune = rune;
    }

    public void Activate()
    {
        rune.Reset();
        rune.Activated = true;
        rune.OnActivate();
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (!rune.Activated) return;
        rune.EventListen(e);
        if (rune.IsSuccessful) { rune.OnSuccess(); rune.Activated = false; return; }
        if (rune.TimeTick(e.ppCost)) { rune.OnFail(); rune.Activated = false; return; }
    }

    public override string ToString() => "";

    public bool Activated => rune.Activated;
    public float Progress => rune.Progress;
}