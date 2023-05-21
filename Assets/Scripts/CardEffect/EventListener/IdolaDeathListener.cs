using System.Collections.Generic;
public class IdolaDeathListener : EventListenerComponent
{
    private List<CardBuff> bufflist;
    int hpMod;
    int atkMod;
    public IdolaDeathListener(string[] args) : base(null)
    {
        int.TryParse(args[0], out hpMod);
        int.TryParse(args[1], out atkMod);
    }
    public IdolaDeathListener(int hpMod, int atkMod) : base(null)
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
        if (e is AfterBuffEvent &&  e.target == card)
        {
            var buffEvent = e as AfterBuffEvent;
            buffEvent.buff.ChangeLifeType(BuffLifeType.Battle);
            
        }
    }

}