public class EndRoom : AbstractRoom
{
    public override RoomType Type => RoomType.End;

    public override AbstractRoom Copy() => new EndRoom();

    public override void Execute()
    {
        // 进入下一个场景
    }
}