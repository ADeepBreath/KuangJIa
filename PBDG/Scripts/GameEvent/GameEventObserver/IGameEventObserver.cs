
/// <summary>
/// 游戏事件观察者
/// </summary>
public abstract class IGameEventObserver
{
    public abstract void Update();
    public abstract void SetSubject(IGameEventSubject Subject);
}
