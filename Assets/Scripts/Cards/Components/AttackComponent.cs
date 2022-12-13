
public class AttackComponent : CardComponent
{

    Ref<int> atkRange;
    public Ref<int> atk;
    Ref<int> initAtk;
    Ref<int> atkTimes;
    Ref<int> canAttack;
    Ref<int> atkCost;
    Ref<int> atkFree;
    public int AtkRange => atkRange.value > 0 ? atkRange.value : 0;
    public bool CanAttack => canAttack.value > 0 && atkTimes.value > 0;
    public int AtkCost => atkCost.value > 0 ? 1 : 0;
    public int Atk => atk.value;
    public bool AtkFree => atkFree.value > 0;
    public int InitAtk => initAtk.value;
    public AttackComponent(int atk)
    {
        this.atk = new Ref<int>(atk);
        this.initAtk = new Ref<int>(atk);
        atkTimes = new Ref<int>(1);
        atkCost = new Ref<int>(1);
        canAttack = new Ref<int>(1);
        this.atkRange = new Ref<int>(1);
        atkFree = new Ref<int>(1);
    }


    public AttackComponent(int atk, int atkRange, int atkTimes, bool canAttack, int atkCost, bool atkFree)
    {
        this.atk = new Ref<int>(atk);
        this.initAtk = new Ref<int>(atk);
        this.atkTimes = new Ref<int>(atkTimes);
        this.atkCost = new Ref<int>(atkCost);
        this.canAttack = new Ref<int>(canAttack ? 1 : 0);
        this.atkRange = new Ref<int>(atkRange);
        this.atkFree = new Ref<int>(atkFree ? 1 : 0);
    }
    public AttackComponent(int atk, int atkRange)
    {
        this.atk = new Ref<int>(atk);
        this.initAtk = new Ref<int>(atk);
        atkTimes = new Ref<int>(1);
        atkCost = new Ref<int>(1);
        canAttack = new Ref<int>(1);
        this.atkRange = new Ref<int>(atkRange);
        atkFree = new Ref<int>(1);
    }
    public override void Init()
    {
        this.atk.value = initAtk.value;
    }



    public void Attack(Card target, bool active)
    {
        int cost = active ? AtkCost : 0;
        var e = new AttackEvent(card, target, cost);
        GameManager.Instance.BroadcastCardEvent(e);

        target.GetComponent<AttackedComponent>().ApplyDamage(card, Atk);

        GameManager.Instance.BroadcastCardEvent(e.EventAfter());
    }

    public override void Add(CardComponent component)
    {
        throw new System.Exception("不能添加两个攻击组件");
    }

    public override string Desc()
    {
        if (Atk > InitAtk) return $"<color=green>{Atk.ToString()}</color>";
        else return $"<color=white>{Atk.ToString()}</color>";
    }
}
