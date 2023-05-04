using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在第n排随机位置召唤两个随从单位
/// </summary>
public class ConditionalRandomSummon : NoTargetCardEffect
{
    int summonCount;
    string summonUnit;
    CardRace race;
    public ConditionalRandomSummon(Card card,string[] paras):base(card)
    {
        int.TryParse(paras[0], out summonCount);
        summonUnit=paras[1];
    }
    public ConditionalRandomSummon(Card card,CardRace race, int summonCount):base(card)
    {
        this.summonCount = summonCount;
        this.race = race;
    }

    public override void Excute()
    {
        Debug.Log("excute start");        
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

        }
        
    }
    public override string ToString()
    {
        return $"随机位置召唤{summonCount}个{summonUnit}";
    }
}