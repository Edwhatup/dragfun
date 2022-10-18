namespace Card
{
    public abstract class PlayerCard : AbstractCard
    {
        public PlayerCardState state;
        public int cost;
        public int targetCount;
        public CardTarget[] cardTargets;
        public PlayerCard(string name, params string[] paras) : base(name, paras)
        {
            this.state = PlayerCardState.InDeck;
            this.cost = 0;
        }
        public virtual void OnTurnStart()
        {

        }
        public virtual void OnTurnEnd()
        {

        }
    }
}