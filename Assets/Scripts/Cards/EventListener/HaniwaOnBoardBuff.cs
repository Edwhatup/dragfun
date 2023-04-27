public class HaniwaOnBoardListener : EventListenerComponent
{
    int hpMod;
    int atkMod;
    int boardAttackCount=0;
    public HaniwaOnBoardListener(string[] args):base(null)
    {
        int.TryParse(args[0], out hpMod);
        int.TryParse(args[1], out atkMod);
    }
    public HaniwaOnBoardListener(int hpMod, int atkMod):base(null)
    {
        this.hpMod = hpMod;
        this.atkMod = atkMod;
    }

    public override string ToString()
    {
        if (hpMod == 0 && atkMod == 0) return "";
        if (hpMod == 0) return $"场上每次入场埴轮随从时+{hpMod}攻击力";
        if (atkMod == 0) return $"场上每次入场埴轮随从时+{hpMod}生命值";
        return $"场上每次入场埴轮随从时，获得+{atkMod}+{hpMod}";
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterSummonEvent && e.source.race==CardRace.Haniwa && e.source!=card)
        {
            var summon = e as AfterSummonEvent;
            //if(card.==) 
            {         
                card.Buff(card,atkMod, hpMod); 
            }
        }
    }

}