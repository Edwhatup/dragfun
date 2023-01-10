public class OnAttackbuffListener : EventListenerComponent
{
    int hpModifierOnAttack;
    int atkModifierOnAttack;
    int boardAttackCount=0;
    public OnAttackbuffListener(string[] args)
    {
        int.TryParse(args[0], out hpModifierOnAttack);
        int.TryParse(args[1], out atkModifierOnAttack);
    }
    public OnAttackbuffListener(int hpModifierOnAttack, int atkModifierOnAttack)
    {
        this.hpModifierOnAttack = hpModifierOnAttack;
        this.atkModifierOnAttack = atkModifierOnAttack;
    }

    public override string ToString()
    {
        if (hpModifierOnAttack == 0 && atkModifierOnAttack == 0) return "";
        if (hpModifierOnAttack == 0) return $"攻击时，获得+{hpModifierOnAttack}攻击力";
        if (atkModifierOnAttack == 0) return $"攻击时，获得+{hpModifierOnAttack}生命值";
        return $"攻击时获得+{atkModifierOnAttack}+{hpModifierOnAttack}";
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is BeforeAttackEvent)
        {
            var attack = e as BeforeAttackEvent;
            boardAttackCount+=1;
            if (attack.source == card)
            {
                card.Buff(card,atkModifierOnAttack, hpModifierOnAttack); 
            }
        }
    }

}