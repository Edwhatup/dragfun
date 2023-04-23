public abstract class AbstractRoom
{
    public abstract RoomType Type { get; }

    public int X { get => x; }
    public int Y { get => y; }

    private int x, y;

    public AbstractRoom(int x, int y)
    {
        this.x = X;
        this.y = Y;
    }

    public abstract void Execute();
}

