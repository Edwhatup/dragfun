public class BuffByBoardAtkCountListener : EventListenerComponent
{
    int hpModifierOnAttack;
    int atkModifierOnAttack;
    int boardAttackCount=0;
    int requireCount;
    public BuffByBoardAtkCountListener(string[] args)
    {
        int.TryParse(args[0], out hpModifierOnAttack);
        int.TryParse(args[1], out atkModifierOnAttack);
        int.TryParse(args[2], out requireCount);
    }
    public BuffByBoardAtkCountListener(int hpModifierOnAttack, int atkModifierOnAttack,int requireCount)
    {
        this.hpModifierOnAttack = hpModifierOnAttack;
        this.atkModifierOnAttack = atkModifierOnAttack;
        this.requireCount=requireCount;
    }

    public override string ToString()
    {
        if (hpModifierOnAttack == 0 && atkModifierOnAttack == 0) return "";
        if (hpModifierOnAttack == 0) return $"攻击时，获得+{hpModifierOnAttack}攻击力";
        if (atkModifierOnAttack == 0) return $"攻击时，获得+{hpModifierOnAttack}生命值";
        return $"场上每攻击{requireCount}次，使其周围的机械单位获得+{atkModifierOnAttack}+{hpModifierOnAttack}";
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterAttackEvent)
        {
            var attack = e as AfterAttackEvent;
            if(card.field.state==BattleState.Survive) boardAttackCount+=1;
            if (boardAttackCount == requireCount)
            {
                var targets = CardManager.Instance.GetRoundFriendUnits(card);
                foreach(Card target in targets)
                {
                    if(target.race==CardRace.Mech) target.Buff(card,atkModifierOnAttack, hpModifierOnAttack); 
                }
                boardAttackCount=0;
            }
        }
    }

}