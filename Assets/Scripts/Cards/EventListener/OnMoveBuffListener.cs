public class OnMoveBuffListener : EventListenerComponent
{
    int hpModifierOnAttack;
    int atkModifierOnAttack;
    public OnMoveBuffListener(string[] args)
    {
        int.TryParse(args[0], out hpModifierOnAttack);
        int.TryParse(args[1], out atkModifierOnAttack);
    }
    public OnMoveBuffListener(int hpModifierOnAttack, int atkModifierOnAttack)
    {
        this.hpModifierOnAttack = hpModifierOnAttack;
        this.atkModifierOnAttack = atkModifierOnAttack;
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterMoveEvent)
        {
            var move = e as AfterMoveEvent;
            if (e.source == card)
            {
                card.Buff(card,atkModifierOnAttack,hpModifierOnAttack);
            }
        }
    }
    public override string ToString()
    {
        if (hpModifierOnAttack == 0 && atkModifierOnAttack == 0)
            return "";
        else if (atkModifierOnAttack == 0 && atkModifierOnAttack != 0)
            return $"移动时，获得+{hpModifierOnAttack}生命值";

        else if (atkModifierOnAttack != 0 && atkModifierOnAttack == 0)
            return $"移动时，获得+{atkModifierOnAttack}攻击力";
        else
            return $"移动时，获得+{hpModifierOnAttack}+{atkModifierOnAttack}";
    }
}
