using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Card
{
    private const string path = "Datas/Player.json";
    public static Player Instance
    {
        get
        {
            if (instance == null) instance = LoadPlayerData(Resources.Load<TextAsset>(path).text);
            return instance;
        }
    }
    private static Player instance;

    public int maxHandCnt;
    public int initDrawCardCnt;
    public int drawCardCntTurn;
    public int drawPPCost;
    public int maxHp;
    public int hp;

    public Dictionary<string, int> deck;
    public List<string> cards = new List<string>();

    public Player() : base(null)
    {
        ReadDeck();
        AddComponent(new AttackedComponent(maxHp));
    }

    public void BindVisual(PlayerVisual playerVisual)
    {
        visual = playerVisual;
    }

    private void ReadDeck()
    {
        deck = new Dictionary<string, int>();
        for (int i = 0; i < cards.Count; i += 2)
        {
            deck[cards[i]] = int.Parse(cards[i + 1]);
        }
    }

    public static Player LoadPlayerData(string s)
    {
        return JsonUtility.FromJson<Player>(s);
    }
}