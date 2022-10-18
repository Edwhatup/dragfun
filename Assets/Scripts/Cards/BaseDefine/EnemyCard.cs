using System.Collections.Generic;
namespace Card.Enemy
{
    public abstract class EnemyCard : AbstractCard
    {
        public int hp;
        public int maxHp;
        public int initMaxHp;
        public BattleState battleState;
        public List<EnemyAction> actions;
        public EnemyCard(string name, int hp, params string[] paras) : base(name, paras)
        {
            this.hp = hp;
            this.maxHp = hp;
            this.initMaxHp = hp;
            this.battleState = BattleState.Survive;
            actions = new List<EnemyAction>();
        }
        public void Heal(AbstractCard source, int healDelta)
        {
            this.hp += healDelta;
            this.hp = this.hp < this.maxHp ? this.hp : this.maxHp;
        }
        public void ApplyDamage(AbstractCard source, int damage)
        {
            this.hp -= damage;
            if (this.hp < 0)
            {
                this.battleState = BattleState.HalfDead;
            }
        }
    }
}
