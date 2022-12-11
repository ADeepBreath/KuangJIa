using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态基类
/// </summary>
public abstract class FSMState
{
    protected string stateName="BaseState";
    public string StateName { get { return stateName; } }
    protected FSMSystem fsm;

    public FSMState(FSMSystem fsm)
    {
        this.fsm = fsm;
    }

    public virtual void DoBeforeEntering() { }
    public virtual void DoAfterLeaving() { }
    public abstract void Act();
    public abstract void Reason();
}

