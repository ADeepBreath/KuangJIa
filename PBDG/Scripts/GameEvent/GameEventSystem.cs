
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏事件名称
/// </summary>
public enum ENUM_GameEvent
{
    Null = 0,
    EnemyKilled = 1,// 地方单位阵亡
    SoldierKilled = 2,// 玩家单位阵亡
    SoldierUpgate = 3,// 玩家单位升级
    NewStage = 4,// 新关卡
}
/// <summary>
/// 游戏事件系统
/// </summary>
public class GameEventSystem:IGameSystem
{
    /// <summary>
    /// 所有游戏事件主题
    /// </summary>
    private Dictionary<ENUM_GameEvent,IGameEventSubject> m_GameEvents=new Dictionary<ENUM_GameEvent,IGameEventSubject>();

    public GameEventSystem(BattleGame battleGame):base(battleGame)
    {
        Initialize();
    }

    /// <summary>
    /// 释放
    /// </summary>
    public override void Release()
    {
        m_GameEvents.Clear();
    }

    /// <summary>
    /// 给某一个主题注册一个观察者
    /// </summary>
    public void RegisterObserver(ENUM_GameEvent emGameEvnet, IGameEventObserver Observer)
    {
        // 取得事件
        IGameEventSubject Subject = GetGameEventSubject(emGameEvnet);
        if (Subject != null)
        {
            Subject.Attach(Observer);
            Observer.SetSubject(Subject);
        }
    }
    /// <summary>
    /// 获取游戏事件主题，没有就注册一个事件
    /// </summary>
    private IGameEventSubject GetGameEventSubject(ENUM_GameEvent emGameEvnet)
    {
        // 是否已经存在
        if (m_GameEvents.ContainsKey(emGameEvnet))
            return m_GameEvents[emGameEvnet];

        // 注册对应的游戏事件
        IGameEventSubject pSujbect = null;
        switch (emGameEvnet)
        {
            case ENUM_GameEvent.EnemyKilled:
                pSujbect = new EnemyKilledSubject();
                break;
            case ENUM_GameEvent.SoldierKilled:
                pSujbect = new SoldierKilledSubject();
                break;
            case ENUM_GameEvent.SoldierUpgate:
                pSujbect = new SoldierUpgateSubject();
                break;
            case ENUM_GameEvent.NewStage:
                pSujbect = new NewStageSubject();
                break;
            default:
                Debug.LogWarning("还没有针对[" + emGameEvnet + "]指定要產生的Subject類別");
                return null;
        }

        // 加入游戏事件主题
        m_GameEvents.Add(emGameEvnet, pSujbect);
        return pSujbect;
    }
    /// <summary>
    /// 通知游戏事件更新
    /// </summary>
    public void NotifySubject(ENUM_GameEvent emGameEvnet, System.Object Param)
    {
        // 是否存在
        if (m_GameEvents.ContainsKey(emGameEvnet) == false)
            return;

        //Debug.Log("SubjectAddCount["+emGameEvnet+"]");
        m_GameEvents[emGameEvnet].SetParam(Param);
    }
}
