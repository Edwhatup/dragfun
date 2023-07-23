using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : Card
{
    public TestSpell(CardInfo info) : base(info)
    {
        name = "用于关卡测试";
        camp = CardCamp.Friendly;
        type = CardType.Spell;
        cost = 0;
        var e = new GroupDamage(this,100,RangeType.AllEnemies);
        AddComponent(new SpellCastComponent(1, e));
        GetDesc=()=>e.ToString();    
    }
}
