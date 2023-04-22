﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBuff : NoTargetCardEffect
{
    int atk;
    int hp;
    public BasicBuff(Card card,int atk, int hp) : base(card)
    {
        this.atk = atk;
        this.hp = hp;
    }
    public override string ToString()
    {
        return $"获得+{atk}/+{hp}";
    }
    public override void Excute()
    { 
        var cell = card.field.cell;
        var cells = CellManager.Instance.GetCells().FindAll(c => c.card.type==CardType.FriendlyDerive);
        foreach (var c in cells)
        {
            card.Buff(card,atk,hp);
        }
    }
}