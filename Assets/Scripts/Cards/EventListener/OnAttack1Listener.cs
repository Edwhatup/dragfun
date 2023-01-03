public class OnAttack1Listener : EventListenerComponent
{
    int hpModifierOnAttack;
    int atkModifierOnAttack;
    public OnAttack1Listener(string[] args)
    {
        int.TryParse(args[0], out hpModifierOnAttack);
        int.TryParse(args[1], out atkModifierOnAttack);
    }
    public OnAttack1Listener(int hpModifierOnAttack, int atkModifierOnAttack)
    {
        this.hpModifierOnAttack = hpModifierOnAttack;
        this.atkModifierOnAttack = atkModifierOnAttack;
    }

    public override string ToString()
    {
        if (hpModifierOnAttack == 0 && atkModifierOnAttack == 0) return "";
        if (hpModifierOnAttack == 0) return $"移动时，获得+{hpModifierOnAttack}攻击力";
        if (atkModifierOnAttack == 0) return $"移动时，获得+{hpModifierOnAttack}生命值";
        return $"移动时获得+{atkModifierOnAttack}+{hpModifierOnAttack}";
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is BeforeAttackEvent)
        {
            var attack = e as BeforeAttackEvent;
            if (attack.source == card)
            {
                card.Buff(card,atkModifierOnAttack, hpModifierOnAttack); 
            }
        }
    }

}