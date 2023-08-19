using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHaniwa : Card
{
    public SoldierHaniwa(CardInfo info) : base(info)
    {
        name = "战士埴轮";
        camp = CardCamp.Friendly;
        type = CardType.Monster;
        race = CardRace.Haniwa;
        rarity = CardRarity.FirstBase;
        cost = 1;
        AddComponent(new AttackComponent(1));
        AddComponent(new AttackedComponent(4));
        AddComponent(new ActionComponent());
        AddComponent(new SummonComponent());
        AddComponent(new FieldComponnet());

        var e=new HaniwaOnBoardListener(1,1);
        AddComponent(e);
        
        var d=new DrawCard(this,1);
        AddComponent(new DeadComponent(d));

        GetDesc = () => "入场时:"+e.ToString()+"\n死亡时："+d.ToString();
    }
}
