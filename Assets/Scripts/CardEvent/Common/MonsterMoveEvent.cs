
using Card.Monster;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

namespace CardEvent
{
    //移动事件，在移动完成后广播
    public class MonsterMoveEvent
    {
        //发起移动的monster
        public MonsterCard sourceMonster;
        //被交换的monster，没有为null
        public MonsterCard targetMonster;
        //初始cell
        public Cell sourceCell;
        //目标cell
        public Cell targetCell;

        public bool IsSwap()
        {
            return targetMonster != null;
        }
        //sourceMonster的移动是否为前进
        public bool IsSourceForward()
        {
            return CellManager.Instance.GetCellRowDistance(targetCell , sourceCell) > 0;
        }
        //targetMonster的移动是否为前进
        public bool IsTargetForward()
        {
            return CellManager.Instance.GetCellRowDistance(sourceCell, targetCell) > 0;
        }
        //反生移动的monster个数
        public int MoveMonsterCount()
        {
            return 1 + (targetMonster == null ? 0 : 1);
        }
        public MonsterMoveEvent(MonsterCard monster, Cell oldCell, Cell newCell)
        {
            this.sourceMonster = monster;
            this.sourceCell = oldCell;
            this.targetCell = newCell;
            targetMonster = null;
        }

        public MonsterMoveEvent(MonsterCard sourceMonster, MonsterCard targetMonster ,Cell oldCell, Cell newCell)
        {
            this.sourceMonster = sourceMonster;
            this.sourceCell = oldCell;
            this.targetCell = newCell;
            this.targetMonster = targetMonster;
        }
    }
}