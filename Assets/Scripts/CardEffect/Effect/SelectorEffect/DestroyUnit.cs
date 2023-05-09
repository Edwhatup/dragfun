using System.Collections.Generic;
using System.Linq;

//破坏！
public class DestroyUnitEffect : CardEffect
{
 
    public DestroyUnitEffect(Card card,string[] paras):base(card)
    {
        
    }

    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add(CardTarget.CardOnBoard );
    }
    public DestroyUnitEffect(Card card) : base(card)
    {
        
    }
    public override void Excute()
    {
        var monster = (Targets[0] as CardVisual).card;
        monster.attacked.Destroy(monster);
    }

    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override string ToString()
    {
        return $"破坏一个场上单位";
    }
}
