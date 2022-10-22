using Card.Enemy;
using Card.Monster;
using Seletion;
namespace Card.Spell
{
    public abstract class SpellCard : PlayerCard,ISeletableSource
    {
        public int TargetCount{get;private set;}
        public CardTarget[] CardTargets {get;private set;}  

        public SpellCard(string _cardName, params string[] _paras) : base(_cardName, _paras) { }
        public abstract void Cast();
        /// <summary>
        /// 当前卡牌是否可以成为第i个目标
        /// </summary>
        /// <param name="card"></param>
        /// <param name="i">base 1</param>
        /// <returns></returns>
        public virtual bool CanSelect(AbstractCard card, int i)
        {
            if (i > TargetCount) return false;
            switch (CardTargets[i - 1])
            {
                case CardTarget.Enemy:
                    return card is EnemyCard;
                case CardTarget.Monster:
                    return card is MonsterCard;
                default:return false;
            }
        }
    }
}