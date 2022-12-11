
using System.Collections.Generic;
/// <summary>
/// 游戏事件主题
/// </summary>
public class IGameEventSubject
{
    private List<IGameEventObserver> m_Observers = new List<IGameEventObserver>();//观察者
    private System.Object m_Param = null;//发生事件时附加得参数

    /// <summary>
    /// 添加观察者
    /// </summary>
    /// <param name="theObserver"></param>
    public void Attach(IGameEventObserver theObserver)
    {
        m_Observers.Add(theObserver);
    }

    /// <summary>
    /// 移除观察者
    /// </summary>
    /// <param name="theObserver"></param>
    public void Detach(IGameEventObserver theObserver)
    {
        m_Observers.Remove(theObserver);
    }

    /// <summary>
    ///  通知观察者
    /// </summary>
    public void Notify()
    {
        foreach (IGameEventObserver theObserver in m_Observers)
            theObserver.Update();
    }
    /// <summary>
    /// 设定参数
    /// </summary>
    /// <param name="Param"></param>
    public virtual void SetParam(System.Object Param)
    {
        m_Param = Param;
    }

}
