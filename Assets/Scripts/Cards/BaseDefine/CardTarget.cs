namespace Card
{
    public enum CardTarget
    {
        None = 0,
        /// <summary>
        /// 入场的敌人
        /// </summary>
        Enemy = 1,
        /// <summary>
        /// 入场的随从
        /// </summary>
        MonsterOnBoard = 2,
        /// <summary>
        /// 手牌中的随从
        /// </summary>
        MonsterOnHand = 4,
        /// <summary>
        /// 手牌中的法术
        /// </summary>
        SpellOnHand = 8,
        /// <summary>
        /// 手牌中的卡牌
        /// </summary>
        CardOnHand = SpellOnHand | MonsterOnHand,

        /// <summary>
        /// 牌库中的随从
        /// </summary>
        MonsterInDeck = 16,
        /// <summary>
        /// 牌库中的法术
        /// </summary>
        SpellInDeck = 32,
        /// <summary>
        /// 牌库中的卡牌
        /// </summary>
        CardInDeck = MonsterInDeck | SpellInDeck,
        /// <summary>
        /// 入场的所有角色
        /// </summary>
        CardOnBoard = Enemy | MonsterOnBoard,
        /// <summary>
        /// 库中的随从
        /// </summary>
        MonsterInLibrary = 64,
        /// <summary>
        /// 库中的法术
        /// </summary>
        SpellInLibrary = 128,
        /// <summary>
        /// 库中的敌人
        /// </summary>
        EnemyInLibrary = 256,
        /// <summary>
        /// 玩家
        /// </summary>
        Player=512,
        /// <summary>
        /// 使用过的随从牌
        /// </summary>
        MonsterUsed=1024,
       /// <summary>
       /// 使用过的法术牌
       /// </summary>
        SpellUsed=2048,
        /// <summary>
        /// 死亡的随从
        /// </summary>
        MonsterInTomb=4096,
        /// <summary>
        /// 死亡的敌人
        /// </summary>
        EnemyInTomb=8192,
        /// <summary>
        /// 使用过的卡牌
        /// </summary>
        CardUsed=MonsterUsed | SpellUsed,
        /// <summary>
        /// 库中的卡牌
        /// </summary>
        CardInLibrary = MonsterInLibrary | SpellInLibrary;
    }
}