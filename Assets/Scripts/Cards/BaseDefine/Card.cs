using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

public class Card
{
    public string name;
    public CardRarity rarity;
    public string imageUrl;
    public CardType type;
    public CardVisual visual;
    public CardCamp camp;
    public Card source;
    public CardState state;
    List<CardComponent> components;
    public string Desc()
    {
        StringBuilder sb = new StringBuilder();
        if (summon != null)
        {
            sb.Append(summon.Desc());
            sb.Append("\n");
        }
        if (cast != null)
        {
            sb.Append(cast.Desc());
            sb.Append("\n");
        }
        if (listener != null)
        {
            sb.Append(listener.GetDesc());
            sb.Append("\n");
        }
        if (dead != null)
        {
            sb.Append(dead.Desc());
            sb.Append("\n");
        }
        return sb.ToString();
    }
    public DeadComponent dead => GetComponent<DeadComponent>();
    public AttackComponent attack => GetComponent<AttackComponent>();
    public AttackedComponent attacked => GetComponent<AttackedComponent>();
    public EventListenerComponent listener => GetComponent<EventListenerComponent>();
    public FieldComponnet field => GetComponent<FieldComponnet>();
    public SummonComponent summon => GetComponent<SummonComponent>();

    public SpellCastComponent cast => GetComponent<SpellCastComponent>();


    public Card(string name)
    {
        this.name = name;
    }

    public ISelector GetSelector()
    {
        if (state == CardState.InHand)
        {
            if (summon != null) return summon;
            if (cast != null) return cast;
        }
        else if (state == CardState.OnBoard)
        {
            return new MonsterFieldSelector(this);
        }
        return null;
    }
    public T GetComponent<T>() where T : CardComponent
    {
        return components.Find((c) => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T))) as T;
    }
    public void AddComponnet(CardComponent component)
    {
        if (components == null) components = new List<CardComponent>();
        var c = components.Find(i => i.GetType() == component.GetType());
        if (c == null)
        {
            components.Add(component);
            component.card = this;
        }
        else c.Add(component);
    }

    public void RemoveComponnent<T>() where T : CardComponent
    {
        components.RemoveAll(c => c.GetType() == typeof(T));
    }
    public void Buff(Card source, int atk, int hp)
    {
        BuffEvent buff = new BuffEvent(source, this);
        EventManager.Instance.PassEvent(buff);
        if (hp != 0)
        {
            this.attacked.hp.value += hp;
            this.attacked.maxHp.value += hp;
        }
        if (atk != 0) this.attack.atk.value += atk;
        EventManager.Instance.PassEvent(buff.EventAfter());

    }
    public List<T> GetComponnets<T>() where T : CardComponent
    {
        return components.FindAll((c) => c.GetType() == typeof(T) || c.GetType().IsSubclassOf(typeof(T)))
                .Select(c => c as T)
                .ToList();
    }
    public override string ToString()
    {
        return name;
    }

    public void Init()
    {
        foreach(var component in components)
        {
            component.Init();
        }
    }
}