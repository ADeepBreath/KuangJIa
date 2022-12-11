using UnityEngine;
/// <summary>
/// 游戏使用界面
/// </summary>
public abstract class IUserInterface
{
    protected BattleGame m_battleGame = null;
    protected GameObject m_RootUI = null;
    private bool m_bActive = true;

    public IUserInterface(BattleGame battleGame)
    {
        m_battleGame = battleGame;
    }

    /// <summary>
    /// 获取激活状态
    /// </summary>
    /// <returns></returns>
    public bool IsVisible()
    {
        return m_bActive;
    }

    /// <summary>
    /// 显示
    /// </summary>
    public virtual void Show()
    {
        m_RootUI.SetActive(true);
        m_bActive = true;
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public virtual void Hide()
    {
        m_RootUI.SetActive(false);
        m_bActive = false;
    }

    public virtual void Initialize() { }
    public virtual void Release() { }
    public virtual void Update() { }
}
