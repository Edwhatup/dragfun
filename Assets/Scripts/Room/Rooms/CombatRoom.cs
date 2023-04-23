public class CombatRoom : AbstractRoom
{
    public override RoomType Type => RoomType.Combat;

    public CombatRoom(int x, int y) : base(x, y)
    {
    }

    public override void Execute()
    {
    }
}