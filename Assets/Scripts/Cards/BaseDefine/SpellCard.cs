namespace Card.Spell
{
    public abstract class SpellCard : PlayerCard
    {
        public SpellCard(string _cardName, params string[] _paras) : base(_cardName, _paras) { }

        public abstract void Cast();
    }
}