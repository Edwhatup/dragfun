using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using UnityEngine;

public abstract class Card
{
    CardInfo info;
    public Card(CardInfo Info)
    {
        this.info = Info;
    }
    public string name;
    public CardRarity rarity;
    public string imageUrl;
    public CardType type;
    public CardVisual visual;
    public CardCamp camp;
    public Card source;
    public CardRace race;
    public Func<string> GetDesc;
    public int cost;
    public int consume = 0;
    List<CardComponent> components;
    public ResonanceComponent resonance => GetComponent<ResonanceComponent>();
    public AttackComponent attack => GetComponent<AttackComponent>();
    public AttackedComponent attacked => GetComponent<AttackedComponent>();
    public FieldComponnet field => GetComponent<FieldComponnet>();
    public UseComponent use => GetComponent<UseComponent>();
    public ActionComponent action => GetComponent<ActionComponent>();
    public EnemyAction enemyAction => GetComponent<EnemyAction>();

    private List<CardBuff> buffs = new List<CardBuff>();
    private List<CardBuff> subBuffs = new List<CardBuff>();
    public List<CardBuff> Buffs => buffs;
    public List<CardBuff> AllBuffs
    {
        get
        {
            List<CardBuff> all = new List<CardBuff>(buffs);
            all.AddRange(subBuffs);
            return all;
        }
    }
    public List<CardBuff> BoardBuffs => buffs.FindAll(i => i.LifeType == BuffLifeType.Board);
    public List<CardBuff> GameBuffs => buffs.FindAll(i => i.LifeType == BuffLifeType.Battle);
    public List<CardBuff> PermanentBuffs => buffs.FindAll(i => i.LifeType == BuffLifeType.Permanent);

    public bool InDeck => CardManager.InDeck(this);
    public bool OnBoard => CardManager.OnBoard(this);
    public bool InHand => CardManager.InHand(this);
    public bool InDiscard => CardManager.InDiscard(this);
    public bool InExile => CardManager.InExile(this);

    public T GetComponent<T>() where T : CardComponent
    {
        return components.Find((c) => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))) as T;
    }
    public void AddComponnet(CardComponent component)
    {
        if (components == null) components = new List<CardComponent>();
        if (CanRepeatAttribute.CanRepeat(component))
        {
            AddComponentWithPreComponent(component);
        }
        else
        {
            var c = components.Find(i => i.GetType() == component.GetType());
            if (c == null)
            {
                AddComponentWithPreComponent(component);
            }
            else return;
            //else throw new Exception($"重复添加不可重复组件{component.GetType().Name}");
        }
    }
    private void AddComponentWithPreComponent(CardComponent component)
    {
        if (components.Find(i => i.GetType() == component.GetType()) == null)
        {
            var preCom = RequireCardComponentAttribute.GetPreComponents(component.GetType());
            foreach (var c in preCom)
            {
                AddComponnet(c);
            }
        }
        components.Add(component);
        component.card = this;
    }
    public void RemoveComponnent<T>() where T : CardComponent
    {
        components.RemoveAll(c => c.GetType() == typeof(T));
    }
    public void RemoveComponnent(CardComponent component)
    {
        components.Remove(component);
    }

    [Obsolete("现在使用新的Buff系统, 考虑拓展CardBuff并使用card.AddBuff")]
    public void Buff(Card source, int atk, int hp)
    {
        if (attacked != null && hp != 0)
        {
            this.attacked.hp += hp;
            this.attacked.maxHp += hp;
        }
        if (attack != null && atk != 0) this.attack.atk += atk;
        AfterBuffEvent buff = new AfterBuffEvent(source, this);
        EventManager.Instance.PassEvent(buff);
    }

    public void AddBuff(CardBuff buff)
    {
        buff.Bind(this);
        buffs.Add(buff);
        EventManager.Instance.PassEvent(new AfterBuffEvent(source, this, buff));
    }

    public void RemoveBuff(CardBuff buff)
    {
        if (!buffs.Contains(buff)) return;
        buffs.Remove(buff);
        buff.Release();
    }

    public void AddSubBuff(CardBuff buff)
    {
        buff.Bind(this);
        subBuffs.Add(buff);
        EventManager.Instance.PassEvent(new AfterBuffEvent(source, this, buff));
    }

    public void RemoveSubBuff(CardBuff buff)
    {
        if (!buffs.Contains(buff)) return;
        subBuffs.Remove(buff);
        buff.Release();
    }

    public void TurnReset()
    {
        foreach (var component in components)
        {
            component.TurnReset();
        }
    }

    public void Recycle()
    {
        Debug.Log($"回收了 {name}");
        components.ForEach(i => i.Recycle());
        BoardBuffs.ForEach(i => RemoveBuff(i));
    }

    public List<T> GetComponnets<T>() where T : CardComponent
    {
        return components.FindAll((c) => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)))
                .Select(c => c as T)
                .ToList();
    }
    public void Init()
    {
        foreach (var component in components)
        {
            component.Init();
        }
    }
    public void AddTag(string name, int times)
    {
        if (tags.ContainsKey(name))
        {
            tags[name] += times;
            if (tags[name] < 0) tags[name] = 0;
        }
        else tags[name] = times;
    }
    public bool ContainsTag(string tag)
    {
        return tags.ContainsKey(tag) && tags[tag] > 0;
    }
    public void ClearTag(string tag)
    {
        if (tags.ContainsKey(tag))
            tags[tag] = 0;
    }
    public Dictionary<string, int> tags = new Dictionary<string, int>();
    //public class CardTag
    //{
    //    string name;
    //    bool canRepeat;
    //    int times;

    //    public CardTag(string name, bool canRepeat, int times)
    //    {
    //        this.name = name;
    //        this.canRepeat = canRepeat;
    //        this.times = times;
    //    }
    //}
    //public List<CardTag> tags = new List<CardTag>();
}