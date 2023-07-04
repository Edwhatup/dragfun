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
        Player.Instance.BindVisual(this);
    }

    public override void UpdateVisual()
    {
        if (healthText) healthText.text = card.attacked.hp + "/" + card.attacked.maxHp;
    }
}