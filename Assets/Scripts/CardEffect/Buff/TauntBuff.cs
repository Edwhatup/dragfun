using UnityEngine;

public class TauntBuff : CardBuff
{

    public TauntBuff() : base("嘲讽", -1, BuffType.Positive, BuffLifeType.Board)
    {
        
    }

    public override void Execute()
    {
        card.attacked.taunt=1;
    }

    public override void Undo()
    {
        card.attacked.taunt=0;
    }

    public override string GetDesc() => $"嘲讽";
}
