using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillComponentBase
{
    protected GameObject model;
    protected long startTime;

    public SkillComponentBase(GameObject model)
    {
        this.model = model;
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// 运行
    /// </summary>
    public virtual void Play()
    {
        startTime = DateTime.Now.Ticks;
    }

    /// <summary>
    /// 停止运行
    /// </summary>
    public virtual void Stop()
    {
        TimeTool.GetInstance().RemoveTimeInvoke(startTime);
    }
}
