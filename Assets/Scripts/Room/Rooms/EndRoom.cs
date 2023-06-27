using UnityEngine;

public class EndRoom : AbstractRoom
{
    public override RoomType Type => RoomType.End;
    public override bool ShowExecButton => false;
    public override bool ExecuteOnEnter => true;

    public override AbstractRoom Copy() => new EndRoom();

    public override void Execute()
    {
        // 进入下一个场景
        Debug.Log("进入下一个场景");
    }
}