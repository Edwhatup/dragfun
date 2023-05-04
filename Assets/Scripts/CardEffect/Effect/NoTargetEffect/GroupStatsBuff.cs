using System.Collections.Generic;
/// <summary>
/// 强化随从
/// </summary>
public class GroupStatsBuff : NoTargetCardEffect
{
    int hpModifier;
    int atkMofifier;
    RangeType rangeType=RangeType.AllFriends;
    CardRace race=CardRace.Any;
    public GroupStatsBuff(Card card, string[] paras) :base(card)
    {
        int.TryParse(paras[0], out hpModifier);
        int.TryParse(paras[1], out atkMofifier);
    }
    public GroupStatsBuff(Card card, int hpModifier, int atkMofifier):base(card)
    {
        this.hpModifier = hpModifier;
        this.atkMofifier = atkMofifier;
    }

    public GroupStatsBuff(Card card, int hpModifier, int atkMofifier,CardRace race):base(card)
    {
        this.hpModifier = hpModifier;
        this.atkMofifier = atkMofifier;
        this.race=race;
    }

    public override string ToString()
    {
        if(rangeType==RangeType.AllFriends)
        {
            if(race==CardRace.Any)return $"使所有场上友方获得+{atkMofifier}+{hpModifier}";
            else return $"使所有场上{race}获得+{atkMofifier}+{hpModifier}" ;
        }
        else
        {
            return"";
        }
    }
    public override void Excute()
    {
        var buff = new StatsPositiveBuff(atkMofifier,hpModifier);
        if(race == CardRace.Any)
        {
            var friends = CardManager.Instance.GetSpecificAreaFriends(card,card,rangeType);
            foreach(var friend in friends)
            {
                friend.AddBuff(buff);
            }
        }
        else
        {
            var friends = CardManager.Instance.GetSpecificAreaFriends(card,card,rangeType);
            foreach(var friend in friends)
            {
                if(friend.race==race) friend.AddBuff(buff);
            }
        }
        
            
    }

    
}