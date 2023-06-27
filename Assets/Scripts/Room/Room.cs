public abstract class AbstractRoom
{
    public abstract RoomType Type { get; }
    public abstract bool ShowExecButton { get; }
    public abstract bool ExecuteOnEnter { get; }

    public abstract void Execute();

    public abstract AbstractRoom Copy();
}

