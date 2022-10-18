using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Card.Spell;
namespace CardEvent
{
    //使用卡牌事件
    public class UseSpellEvent
    {
        public SpellCard card;
        public UseSpellEvent(SpellCard card)
        {
            this.card = card;
        }
    }

}