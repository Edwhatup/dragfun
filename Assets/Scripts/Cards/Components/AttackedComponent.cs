
public class AttackedComponent : CardComponent
{
    public int Hp => hp.value;
    public Ref<int> hp;
    public int InitMaxHp => initMaxHp.value;
    public Ref<int> initMaxHp;
    public int MaxHp => maxHp.value;
    public Ref<int> maxHp;
    public int Bless => bless.value;
    public Ref<int> bless;
    public AttackedComponent(int maxHp)
    {
        this.hp = new Ref<int>(maxHp);
        this.initMaxHp = new Ref<int>(maxHp);
        this.maxHp = new Ref<int>(maxHp);
    }
    public AttackedComponent(int maxHp, int bless) : this(maxHp)
    {
        if (bless >= 0)
            this.bless = new Ref<int>(bless);
    }
    public void Heal(Card source, int delta)
    {
        var e=new HealEvent(source,card,delta);
        EventManager.Instance.PassEvent(e);
        this.hp.value += delta;
        if(Hp>MaxHp) hp.value = MaxHp;
        EventManager.Instance.PassEvent(e);
    }
    public BattleState ApplyDamage(Card source, int damage)
    {
        var e = new DamageEvent(source, card, damage);
        EventManager.Instance.PassEvent(e);
        if (bless != null)
        {
            damage = bless.value < damage ? bless.value : damage;
            bless = null;
        }
        this.hp.value -= damage;
        if (this.Hp <= 0)
            CardManager.Instance.DestoryCardOnBoard(source,card);
        EventManager.Instance.PassEvent(e.EventAfter());
        return card.field.state;
    }
    public BattleState TryApplyDamage(Card source, int damage)
    {
        if (bless != null && damage > bless.value)
        {
            damage = bless.value;
        }
        if (this.Hp - damage <= 0)
            return BattleState.Dead;
        return BattleState.Survive;
    }

    public override void Add(CardComponent component)
    {
        throw new System.Exception("不能添加两个受击组件");
    }

    public override string Desc()
    {
        if(this.Hp==this.MaxHp)
        {
            if (this.MaxHp > this.InitMaxHp)
                return $"<color=green>{this.Hp}</color>";
            else return $"<color=white>{this.Hp}</color>";
        }
        return $"<color=red>{this.Hp}</color>";
    }
}
