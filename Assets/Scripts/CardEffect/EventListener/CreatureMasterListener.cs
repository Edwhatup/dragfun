public class CreatureMasterListener : EventListenerComponent
{
    int onboardcnt;
    int buffCnt;
    public CreatureMasterListener(string[] args) : base(null)
    {
        int.TryParse(args[0], out onboardcnt);
        int.TryParse(args[1], out buffCnt);
    }
    public CreatureMasterListener(int onboardcnt,int buffCnt) : base(null)
    {
        this.onboardcnt = onboardcnt;
        this.buffCnt = buffCnt;
    }

    public override string ToString()
    {
        return $"场上{onboardcnt}次入场埴轮随从时随机使场上的{buffCnt}个埴轮随从获得造型强化";

    }

    public override void EventListen(AbstractCardEvent e)
    {
        int i=0;

        if (e is AfterSummonEvent && e.source.race == CardRace.Haniwa && e.source != card)
        {
            i+=1;
            if(i==onboardcnt)
            {
                var targets = CardManager.Instance.board.FindAll(c => c.race==CardRace.Haniwa).GetRandomItems(buffCnt);
                foreach (var target in targets)
                {
                    target.AddBuff(new CreatureReinforceBuff());
                }
                i=0;
            }

        }
    }

}