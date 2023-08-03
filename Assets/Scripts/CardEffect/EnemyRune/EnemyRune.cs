public abstract class EnemyRune : CardEffect
{
    public bool Activated { get; set; }

    private float requiredProgress, progress = 0;
    private int lifeTime, timer = 0;

    public float Progress => progress / requiredProgress;

    protected EnemyRune(Card card, float requiredProgress = 1, int lifeTime = 1) : base(card)
    {
        this.requiredProgress = requiredProgress;
        this.lifeTime = lifeTime;
    }

    public bool TimeTick(int pp)
    {
        timer += pp;
        return lifeTime > 0 && timer >= lifeTime;
    }

    public void Reset()
    {
        progress = 0;
        timer = 0;
    }

    public bool IsSuccessful => progress >= requiredProgress;

    public override sealed void Excute() { }
    public override sealed void InitTarget() { }

    /// <summary>
    /// 重写这个方法来定义符卡条的“充能”行为
    /// 即：先判断 e 是不是需要的事件，然后根据事件给出的值来增加 progress 这个变量
    /// </summary>
    public abstract void EventListen(AbstractCardEvent e);

    /// <summary>
    /// 当这个符卡被激活时的回调，可以给敌人加Buff啥的
    /// </summary>
    public abstract void OnActivate();

    /// <summary>
    /// 当这个符卡条成功被玩家达成时触发
    /// </summary>
    public abstract void OnSuccess();

    /// <summary>
    /// 当这个符卡条的时间期限到了时触发
    /// </summary>
    public abstract void OnFail();
}