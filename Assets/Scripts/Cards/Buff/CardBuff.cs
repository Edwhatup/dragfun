public class CardBuff
{
    public string Name => name;
    public int LifeTime => lifeTime;
    public int LifeTimer => lifeTimer;
    public BuffType Type => type;
    public BuffLifeType LifeType => lifeType;

    protected string name;

    // lifeTime置于-1时，Buff不会随着pp消失
    protected int lifeTime, lifeTimer;
    protected BuffType type;
    protected BuffLifeType lifeType;

    protected Card card;

    public CardBuff(string name, int lifeTime, BuffType type, BuffLifeType lifeType)
    {
        this.name = name;
        this.lifeTime = lifeTime;
        this.lifeTimer = lifeTime;
        this.type = type;
        this.lifeType = lifeType;
        EventManager.Instance.eventListen += Listen;
    }

    public void Bind(Card c)
    {
        card = c;
        Execute();
    }

    public virtual void Execute() { }
    public virtual void Undo() { }

    public void Release()
    {
        EventManager.Instance.eventListen -= Listen;
        Undo();
    }

    public void Listen(AbstractCardEvent e)
    {
        if (e.ppCost != 0 && lifeTime != -1)
        {
            lifeTimer -= e.ppCost;
            if (lifeTimer <= 0) card.RemoveBuff(this);
        }
    }

    public virtual string GetDesc()
    {
        return $"{name}: {lifeTimer}";
    }
}

public enum BuffType
{
    Positive, Negative, Neutral
}

public enum BuffLifeType
{
    // 在场时存在，下场时消除
    Board,

    // 当局游戏存在，下一局消除
    Game,

    // 始终存在
    Permanent
}
