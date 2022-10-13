using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAction
{
    public Card sourse;
    public ActionTarget actionTarget;
    public AbstractAction(Card soure)
    {
        this.sourse = soure;
        actionTarget = ActionTarget.Self;
    }

    protected AbstractAction(Card sourse, ActionTarget actionTarget) : this(sourse)
    {
        this.actionTarget = actionTarget;
    }

    public abstract void Execute();
    public enum ActionTarget
    {
        Self,
        All,
        Specify,
    }
}
