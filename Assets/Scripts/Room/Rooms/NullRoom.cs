using UnityEngine;

public class NullRoom : AbstractRoom
{
    public override RoomType Type => RoomType.Null;
    public override bool ShowExecButton => false;
    public override bool ExecuteOnEnter => false;

    public override AbstractRoom Copy() => new NullRoom();

    public override void Execute()
    {
        Debug.LogError("错误: 空房间没有被替换掉!");
    }
}
