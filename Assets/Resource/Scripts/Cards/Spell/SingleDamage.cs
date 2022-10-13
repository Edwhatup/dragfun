using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDamage : SpellCard
{
    public int damage;
    public SingleDamage(string _cardName, params string[] _paras) : base(_cardName, _paras)
    {
        ParseHelper.CheckParamsCountWithMessage(_paras, 1, nameof(SingleDamage), _cardName);
        damage = ParseHelper.ParseCardIntWithMessage(_paras[0], nameof(SingleDamage), _cardName);
    }

    public override void Cast()
    {

    }

    public override string GetDesc()
    {
        return $"对一名敌人造成{damage}点伤害";
    }
}
