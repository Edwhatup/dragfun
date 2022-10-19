using Card.Monster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace CardEvent
{
    //交换事件，在交换完成后广播
    public class SwapEvent
    {
        public MonsterCard sourceCard;
        public MonsterCard targetCard;
        public Cell sourceCell;
        public Cell targetCell;

        public SwapEvent(MonsterCard sourceCard, MonsterCard targetCard, Cell sourceCell, Cell targetCell)
        {
            this.sourceCard = sourceCard;
            this.targetCard = targetCard;
            this.sourceCell = sourceCell;
            this.targetCell = targetCell;
        }
    }

}