using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player:Card
{


    public static Player Instance { get; set; }

    public int maxHandCnt;
    public int initDrawCardCnt;
    public int drawCardCntTurn;
    public int maxHp;
    public int hp;

    public Dictionary<string, int> deck;
    public List<string> cards = new List<string>();

    public Player(string name):base(null)
    {

    }
    public void Init(PlayerVisual playerVisual)
    {
        Instance = this;
        visual = playerVisual;
        ReadDeck();
        AddComponnet(new AttackedComponent(maxHp));
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