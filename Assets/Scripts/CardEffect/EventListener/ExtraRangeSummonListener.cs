public class ExtraRangeSummonListener : EventListenerComponent
{
    string summonTarget;
    int boardSummonCount=0;
    int requireCount;
    public ExtraRangeSummonListener(CardEffect effect,int requireCount):base(effect)
    {
        this.effect=effect;
        this.requireCount=requireCount;
    }

    public override string ToString()
    {
        return $"如果入场时，入场过的带有增程的随从超过{requireCount}"+effect.ToString();
    }

    public override void EventListen(AbstractCardEvent e)
    {
        if (e is AfterSummonEvent)
        {
            var summon = e as AfterSummonEvent;
            if(card.field.state==BattleState.Survive && summon.source.attack.atkRange>1) boardSummonCount+=1;
            if (boardSummonCount == requireCount)
            {
                Excute();
                boardSummonCount=0;
            }
        }
    }

}