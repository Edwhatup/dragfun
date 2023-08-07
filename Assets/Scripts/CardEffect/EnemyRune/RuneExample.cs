public class RuneExample : EnemyRune
{
    public RuneExample(Card card, float requiredProgress = 1, int lifeTime = 1) : base(card, requiredProgress, lifeTime)
    {
    }

    public override void EventListen(AbstractCardEvent e)
    {
        // 如果一个单位被自己击杀
        if (e is DeathEvent d && d.source == card)
        {
            // 生成恶灵
            // 这里的卡牌种类可以交给构造函数来输入，不必直接使用字符串
            var info = new CardInfo() { name = "恶灵" };
            var card = CardStore.Instance.CreateCard(info);
            card.field.Summon(d.target.field.cell);
        }

        // 如果被攻击，则累积进度
        if (e is AfterAttackEvent a && a.target == card)
            progress += a.damageInfo.actualDamage;

    }

    // 符卡被激活，可以给card加点buff啥的
    // 这个例子里没有，所以啥都不干
    public override void OnActivate() { }

    // 倒计时结束，应当清空在OnActivate时赋予card的特殊效果
    // 在这个例子里没有，所以啥都不干
    public override void OnFail() { }

    // 累计伤害达到目标，应当给予玩家奖励
    // 但 “战斗结束后获取额外奖励” 这个系统还没做，所以先空着
    public override void OnSuccess()
    {

    }
}