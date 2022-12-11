using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeInvoke
{
    public long startTime;//��DateTime.Now.Ticks���ж����ݣ����ÿ��ԣ�������ID������
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
/// ��ʱ����
/// </summary>
public class TimeTool:Singleton<TimeTool>
{
    List<TimeInvoke> timeInvokes = new List<TimeInvoke>();

    /// <summary>
    /// �����ʱ����
    /// </summary>
    /// <param name="delayTime">��ʱʱ��</param>
    /// <param name="callBack">�ص�����</param>
    public void AddTimeInvoke(long startTime,float delayTime,Action callBack)
    {
        timeInvokes.Add(new TimeInvoke(startTime, delayTime, callBack));
    }

    /// <summary>
    /// �Ƴ���ʱ����
    /// </summary>
    /// <param name="startTime">��ʼʱ��</param>
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
        //��ʱ��ʱ����
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
