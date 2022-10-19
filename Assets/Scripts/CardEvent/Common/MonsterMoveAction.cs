
using Card.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace CardEvent
{
    //移动事件，在移动完成后广播
    public class MonsterMoveAction
    {
        public MonsterCard monster;
        public Cell oldCell;
        public Cell newCell;

        public MonsterMoveAction(MonsterCard monster, Cell oldCell, Cell newCell)
        {
            this.monster = monster;
            this.oldCell = oldCell;
            this.newCell = newCell;
        }
    }
}