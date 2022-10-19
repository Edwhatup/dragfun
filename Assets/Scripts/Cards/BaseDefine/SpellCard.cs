namespace Card.Spell
{
    public abstract class SpellCard : PlayerCard
    {
        public int targetCount;
        public CardTarget[] cardTargets;
        public SpellCard(string _cardName, params string[] _paras) : base(_cardName, _paras) { }

        public abstract void Cast();
    }
}