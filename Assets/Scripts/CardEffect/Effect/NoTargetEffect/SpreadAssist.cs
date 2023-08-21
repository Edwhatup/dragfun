using UnityEngine;

public class SpreadAssist : PassiveCardEffect
{
    private int atk, hp, ed;

    public SpreadAssist(Card card, int atk, int hp, int extraDamage) : base(card)
    {
        this.atk = atk;
        this.hp = hp;
        ed = extraDamage;
    }

    public override void HandleEvent(AbstractCardEvent e)
    {
        if (!CardManager.OnBoard(card)) return;
        if (e.source == card) return;
        if (e is AfterUseEvent u)
        {
            if (CellManager.Instance.GetStreetDistance(card.field.cell, u.source.field.cell) == 1)
            {
                u.source.AddComponent(new PassiveEffectComponent(new SpreadAssist(u.source, atk, hp, ed)));
                u.source.AddBuff(new StatsPositiveBuff(atk, hp, ed));
            }
        }
    }

    public override string ToString()
    {
        return $"应援：+1/+1,并使得被应援的随从获得这个效果";
    }

}