using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInvoke
{
    public long startTime;//以DateTime.Now.Ticks做判断依据，民用可以，建议用ID管理器
    public float delayTime;
    public Action callBack;

    public TimeInvoke(long startTime, float delayTime, Action callBack)
    {
        this.startTime = startTime;
        this.delayTime = delayTime;
        this.callBack = callBack;
    }
}
/// <summary>
/// 计时工具
/// </summary>
public class TimeTool:Singleton<TimeTool>
{
    List<TimeInvoke> timeInvokes = new List<TimeInvoke>();

    /// <summary>
    /// 添加延时方法
    /// </summary>
    /// <param name="delayTime">延时时间</param>
    /// <param name="callBack">回调方法</param>
    public void AddTimeInvoke(long startTime,float delayTime,Action callBack)
    {
        timeInvokes.Add(new TimeInvoke(startTime, delayTime, callBack));
    }

    /// <summary>
    /// 移除延时方法
    /// </summary>
    /// <param name="startTime">起始时间</param>
    public void RemoveTimeInvoke(float startTime)
    {
        for (int i = timeInvokes.Count-1; i >=0; i--)
        {
            if (timeInvokes[i].startTime==startTime)
            {
                timeInvokes.RemoveAt(i);
                break;
            }
        }
    }

    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        //延时计时方法
        if(timeInvokes.Count>0)
        {
            for (int i = timeInvokes.Count - 1; i >= 0; i--)
            {
                long currTimes =DateTime.Now.Ticks - timeInvokes[i].startTime;
                if (currTimes >= (long)(timeInvokes[i].delayTime*10000000))
                {
                    timeInvokes[i].callBack();
                    timeInvokes.RemoveAt(i);
                }
            }
        }

    }
}
