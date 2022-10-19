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
        public int atkTimes;
        public BattleState battleState;
        public MonsterCard(string name, int atk, int hp, params string[] paras) : base(name, paras)
        {
            this.atk = atk;
            this.initAtk = atk;
            this.atkRange = 1;
            atkTimes = 1;
            this.hp = hp;
            this.maxHp = hp;
            this.initMaxHp = hp;
            this.battleState = BattleState.Survive;
        }
        public bool CanAttack()
        {
            return atkTimes > 0;
        }
        public override void OnTurnStart()
        {
            atkTimes=1;
        }
    }
}