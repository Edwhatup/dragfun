public class OnMove1Listener : EventListenerComponent
{
    int hpModifierOnAttack;
    int atkModifierOnAttack;
    public OnMove1Listener(string[] args)
    {
        int.TryParse(args[0], out hpModifierOnAttack);
        int.TryParse(args[1], out atkModifierOnAttack);
    }
    public OnMove1Listener(int hpModifierOnAttack, int atkModifierOnAttack)
    {
        this.hpModifierOnAttack = hpModifierOnAttack;
        this.atkModifierOnAttack = atkModifierOnAttack;
    }

    protected override void EventListen(AbstractCardEvent e)
    {
        if (e.type==AbstractCardEvent.CardEventType.After && e is MoveEvent)
        {
            var move = e as MoveEvent;
            if (e.source == card)
            {
                card.Buff(card,atkModifierOnAttack,hpModifierOnAttack);
            }
        }
    }
    public override string Desc()
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
