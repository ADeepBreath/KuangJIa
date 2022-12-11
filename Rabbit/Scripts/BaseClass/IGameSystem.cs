                                                                                                                                                                     
/// <summary>
/// 游戏系统基类
/// </summary>
public class IGameSystem
{
    protected Game m_Game;

    public IGameSystem(Game game)
    {
        m_Game = game;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}