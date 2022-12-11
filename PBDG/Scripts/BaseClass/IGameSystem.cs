/// <summary>
/// 游戏子系统共用界面
/// </summary>
public abstract class IGameSystem
{
    protected BattleGame m_battleGame = null;
    public IGameSystem(BattleGame battleGame)
    {
        m_battleGame = battleGame;
    }
    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }

}
