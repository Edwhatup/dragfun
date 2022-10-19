
using Card.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace CardEvent
{
    //移动事件
    public class MonsterMoveEvent
    {
        public MonsterCard monster;
        public Cell oldCell;
        public Cell newCell;

        public MonsterMoveEvent(MonsterCard monster, Cell oldCell, Cell newCell)
        {
            this.monster = monster;
            this.oldCell = oldCell;
            this.newCell = newCell;
        }
    }
}