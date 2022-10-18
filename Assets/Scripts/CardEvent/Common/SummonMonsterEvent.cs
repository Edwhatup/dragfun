using Card.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace CardEvent
{
    //召唤事件
    public class SummonMonsterEvent
    {
        public MonsterCard monster;
        public Cell summonCell;
        public SummonMonsterEvent(MonsterCard monster, Cell summonCell)
        {
            this.monster = monster;
            this.summonCell = summonCell;
        }
    }

}
