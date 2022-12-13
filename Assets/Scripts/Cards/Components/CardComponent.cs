using System;

public abstract class CardComponent
{
    public Card card;
    public abstract string Desc();
    public abstract void Add(CardComponent component);
    public virtual void Init()
    {

    }
}