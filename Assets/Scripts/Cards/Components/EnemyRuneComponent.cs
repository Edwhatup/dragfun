[CanRepeat(false)]
public class EnemyRuneComponent : EventListenerComponent
{
    private EnemyRune rune;
    private int actionCnt;
    private int timer = 0;

    public EnemyRuneComponent(EnemyRune rune, int actionCnt)
    {
        this.rune = rune;
        this.actionCnt = actionCnt;
    }

    public void Activate()
    {
        rune.Reset();
        rune.Activated = true;
        rune.OnActivate();
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (!rune.Activated)
        {
            if (timer >= actionCnt) return;
            if (e is EnemyActionEvent a && a.source == card) timer += e.ppCost;
            if (timer >= actionCnt) Activate();

        }
        else
        {
            rune.EventListen(e);
            if (rune.IsSuccessful) { rune.OnSuccess(); rune.Activated = false; return; }
            if (rune.TimeTick(e.ppCost)) { rune.OnFail(); rune.Activated = false; return; }
        }
    }

    public override string ToString() => "";

    public bool Activated => rune.Activated;
    public float Progress => rune.Progress;
}