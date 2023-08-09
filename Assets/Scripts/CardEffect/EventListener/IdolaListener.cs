public class IdolaListener : EventListenerComponent
{

    public IdolaListener() : base(null)
    {

    }

    public override string ToString()
    {
        return $"对其施加的正面效果都持续到整场战斗结束。";

    }

    public override void EventListen(AbstractCardEvent e)
    {

        if (e is AfterBuffEvent && e.target == card )
        {
            AfterBuffEvent ae = e as AfterBuffEvent;
            if(ae.buff.Type==BuffType.Positive && ae.buff.LifeType==BuffLifeType.Board)
            {
                ae.buff.ChangeLifeType(BuffLifeType.Battle);
            }


        }
    }

}