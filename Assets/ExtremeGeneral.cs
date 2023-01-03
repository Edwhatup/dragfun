using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardEvent;
using Core;

namespace Card.Monster
{
    
    public class ExtremeGeneral : MonsterCard
    {
        int hpModifierOnForward;
        int atkModifierOnForward;

        public ExtremeGeneral(string name, int atk ,int hp,params string[] paras): base(name, atk, hp, paras)
        {
            int.TryParse(paras[0], out hpModifierOnForward);
            int.TryParse(paras[1], out atkModifierOnForward);
        }

        [EventListener]
        public void OnSummon(object o)
        {
            SummonMonsterEvent summonEvent = o as SummonMonsterEvent;
            if(summonEvent != null && summonEvent.monster==this)
            {
                //Debug.Log("OnForward");
                int onBoardCount= CardManager.Instance.board.Count;
                for(int i=0; i<onBoardCount;i++)
                {
                    BattleManager.Buff(this, this, hpModifierOnForward, atkModifierOnForward);
                }
            }
        }
        public override string GetDesc()
        {
            return $"入场时，场上每有一个随从（包括自己），获得+{hpModifierOnForward}/+{atkModifierOnForward}";
        }
    }
}
