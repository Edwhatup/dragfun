using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 随从
/// </summary>
public class RebirthEffect : NoTargetCardEffect
{
    int summonCount;
    public RebirthEffect(Card card, string[] paras) :base(card)
    {
        
    }
    public RebirthEffect(Card card,int summonCount):base(card)
    {
       this.summonCount=summonCount;
    }

    public override string ToString()
    {
        return $"随机召唤一个弃牌堆中的埴轮回到场上，并使得其随机造型强化一次。";
    }
    public override void Excute()
    {
        List<Card> conditionedMonsters = CardManager.Instance.GetSpecificDiscardMonster(CardRace.Haniwa,summonCount);
        
        if(conditionedMonsters!=null)
        {
            foreach(var monster in conditionedMonsters)
            {
                var info = new CardInfo()
                {
                    name = monster.name
                };
                Cell cell = CellManager.Instance.GetCells()
                                                .FindAll(c => c.CanSummon())
                                                .GetRandomItem();
                CardManager.Instance.discardDeck.Transfer(CardManager.Instance.board, monster);

                var card = CardStore.Instance.CreateCard(info);
                monster.attacked.Recycle();
                monster.field.Summon(cell);
                monster.AddBuff(new CreatureReinforceBuff());
            } 
        }
        else
        {
            Debug.Log("真没货了");
        }
           
    }
}