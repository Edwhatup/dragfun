using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card;
namespace CardEvent
{
    //死亡事件
    public class DeathEvent
    {
        public AbstractCard dead;
        public AbstractCard killer;
        public DeathEvent(AbstractCard dead, AbstractCard killer)
        {
            this.dead = dead;
            this.killer = killer;
        }
    }
}

