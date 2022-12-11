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
    /// ��ʼ��
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    public virtual void Play()
    {
        startTime = DateTime.Now.Ticks;
    }

    /// <summary>
    /// ֹͣ����
    /// </summary>
    public virtual void Stop()
    {
        TimeTool.GetInstance().RemoveTimeInvoke(startTime);
    }
}
