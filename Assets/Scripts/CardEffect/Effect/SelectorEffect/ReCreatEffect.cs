using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在第n排随机位置召唤两个随从单位
/// </summary>
public class ReCreateEffect : CardEffect
{
    int summonCount;
    string summonUnit;
    CardRace race;
    public ReCreateEffect(Card card,string[] paras):base(card)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
    }
    public override bool CanUse()
    {
        //return CardManager.Instance.board.FindAll(c => c.field.row == 1).Count!=CellManager.Instance.GetColCount();
        return CellManager.Instance.GetCells().FindAll(c => c.CanSummon()).Count >= summonCount;
    }
    public ReCreateEffect(Card card,CardRace race, int summonCount):base(card)
    {
        this.summonCount = summonCount;
        this.race = race;
        InitTarget();
    }
    public override void InitTarget()
    {
        TargetCount = 1;
        CardTargets.Add( CardTarget.Cell);
    }
    public override void OnSelected()
    {
        Selections.Instance.CreateArrow(card.visual.transform);
    }

    public override void Excute()
    {
        //Debug.Log("excute start");        
        List<Card> conditionedMonsters = CardManager.Instance.GetSpecificDeckMonster(race,summonCount);
        
        foreach(var monster in conditionedMonsters)
        {
            var info = new CardInfo()
            {
                name = monster.name
            };
            Cell cell = CellManager.Instance.GetCells()
                                            .FindAll(c => c.CanSummon())
                                            .GetRandomItem();
            CardManager.Instance.drawDeck.Transfer(CardManager.Instance.board, monster);

            var card = CardStore.Instance.CreateCard(info);
            monster.field.Summon(cell);
            monster.attacked.Destroy(monster);

        }
        
    }
    public override string ToString()
    {
        return $"在指定位置从牌堆中召唤{summonCount}个埴轮随从然后将其破坏";
    }
}