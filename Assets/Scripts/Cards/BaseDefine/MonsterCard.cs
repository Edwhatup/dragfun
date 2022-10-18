namespace Card.Monster
{
    public abstract class MonsterCard : PlayerCard
    {
        public int atk;
        public int initAtk;
        public int atkRange;
        public int hp;
        public int maxHp;
        public int initMaxHp;
        public BattleState battleState;
        public MonsterCard(string name, int atk, int hp, params string[] paras) : base(name, paras)
        {
            this.atk = atk;
            this.initAtk = atk;
            this.atkRange = 1;
            this.hp = hp;
            this.maxHp = hp;
            this.initMaxHp = hp;
            this.battleState = BattleState.Survive;
        }
        public void Buff(AbstractCard source, int hpModifier, int atkModifier)
        {
            this.atk += atkModifier;
            this.maxHp += hpModifier;
            this.initMaxHp += hpModifier;
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