public abstract class AbstractRoom
{
    public abstract RoomType Type { get; }

    public abstract void Execute();

    public abstract AbstractRoom Copy();
}

