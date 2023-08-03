using System;

public abstract class CardComponent
{
    public Card card;

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// 回合开始时的重置（行动次数啥的）
    /// </summary>
    public virtual void TurnReset()
    {

    }

    /// <summary>
    /// 卡进入弃牌堆之后重置
    /// </summary>
    public virtual void Recycle()
    {
        
    }
}
