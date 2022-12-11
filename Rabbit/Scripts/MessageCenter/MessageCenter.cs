using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息中心
/// </summary>
public class MessageCenter
{
    private static MessageCenter _instance;
    public static MessageCenter Instance
    {
        get
        {
            if (_instance == null)
                _instance = new MessageCenter();
            return _instance;
        }
    }

    Dictionary<MessageID,List<IObserver>> Observers=new Dictionary<MessageID, List<IObserver>>();

    /// <summary>
    /// 注册观察者
    /// </summary>
    /// <param name="id"></param>
    /// <param name="observer"></param>
    public void RegisterObservers(MessageID id,IObserver observer)
    {
        if(!Observers.ContainsKey(id))
        {
            Observers.Add(id, new List<IObserver>());
        }
        if (Observers[id].Contains(observer))
        {
            Debug.LogError("重复添加观察者");
            return;
        }
        Observers[id].Add(observer);
    }
    /// <summary>
    /// 移除观察者
    /// </summary>
    /// <param name="id"></param>
    /// <param name="observer"></param>
    public void RemoveObservers(MessageID id,IObserver observer)
    {
        if (!Observers.ContainsKey(id))
        {
            Debug.LogError("移除了不存在的事件：" + id);
            return;
        }
        Observers[id].Remove(observer);
        if (Observers[id].Count <= 0)
        {
            Observers.Remove(id);
        }
        return;
    }
    /// <summary>
    /// 通知观察者
    /// </summary>
    public void NotifyObserver(MessageID id)
    {
        if (!Observers.ContainsKey(id))
        {
            Debug.LogError("不存在此事件：" + id);
            return;
        }
        foreach (var item in Observers[id])
        {
            item.HandleNotification(id);
        }
    }

}
