using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVisual : CardVisual
{
    public TextAsset data;

    public Text healthText;

    void Awake()
    {
        card = Player.LoadPlayerData(data.text);
        (card as Player).Init(this);
    }
    public override void UpdateVisual()
    {
        if (healthText) healthText.text = card.attacked.Hp + "/" + card.attacked.MaxHp;
    }
}