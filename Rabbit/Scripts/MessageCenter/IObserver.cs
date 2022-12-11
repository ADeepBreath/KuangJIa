using System;
/// <summary>
///观察者接口
/// </summary>
public interface IObserver
{
    public void HandleNotification(MessageID id);
}
