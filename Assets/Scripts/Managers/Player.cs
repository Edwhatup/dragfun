using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core
{
    public class Player : MonoSingleton<Player>
    {
        [SerializeField]
        TextAsset playerData;
        public int maxHp;
        [HideInInspector]
        public int hp;
        public event Action OnHpChanged;

        internal void ApplyDamage(int damage)
        {
            this.hp -= damage;
            OnHpChanged?.Invoke();
            if (hp <= 0)
            {
                GameManager.Instance.GameFalse();
            }
        }

        public int maxHandCnt;
        public int HandCnt => CardManager.Instance.hand.Count;
        public int initDrawCardCnt;
        public int drawCardCntTurn;
        public Dictionary<string, int> deck = new Dictionary<string, int>();
        [HideInInspector]
        public int coins;
        new void Awake()
        {
            base.Awake();
            Init();
        }
        void Init()
        {
            hp = maxHp;
            LoadPlayerData();
        }
        private void LoadPlayerData()
        {
            string[] dataRow = playerData.text.Split('\n');
            deck.Clear();
            foreach (var row in dataRow)
            {
                if (row.Length == 0) continue;
                string[] rowArray = row.Split(',');
                if (rowArray[0] == "#")
                {
                    continue;
                }
                else if (rowArray[0] == "coins")
                {
                    coins = int.Parse(rowArray[1]);
                }

                else if (rowArray[0] == "card")
                {
                    string name = rowArray[1];
                    int cnt = int.Parse(rowArray[2]);
                    deck[name] = cnt;
                }
            }
        }
    }
}