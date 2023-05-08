using UnityEngine;

public class SwiftBuff : CardBuff
{

    public SwiftBuff() : base("迅捷", -1, BuffType.Positive, BuffLifeType.Board)
    {
        
    }

    public override void Execute()
    {
        card.AddTag("迅捷",1);
    }

    public override void Undo()
    {
        card.ClearTag("迅捷");
    }

    public override string GetDesc() => $"迅捷";
}
