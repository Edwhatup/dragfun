using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Card
{
    public enum CardRarity
    {
        Normal,
        Rare,
    }
    public abstract class AbstractCard
    {
        public string name;
        public string[] paras;
        public CardRarity rarity;
        public string imageUrl;

        public CardVisual visual;


        List<CardComponent> components=new List<CardComponent>();
        public AbstractCard(string name, params string[] paras)
        {
            this.name = name;
            this.paras = paras;
        }
        public virtual string GetDesc()
        {
            return "";
        }

        public T GetCardComponent<T>()where T : CardComponent
        {
            return components.Find((c) => c.GetType() == typeof(T)) as T;
        }
    }
}