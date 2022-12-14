using UnityEngine;
using System.Collections;

/// <summary>
/// 成就系統
/// </summary>
public class AchievementSystem : IGameSystem
{
    private AchievementSaveData m_LastSaveData = null; //最后一次的存档信息

    //记录的成就项目
    private int m_EnemyKilledCount = 0;
    private int m_SoldierKilledCount = 0;
    private int m_StageLv = 0;

    public AchievementSystem(BattleGame battleGame) : base(battleGame)
    {
        Initialize();
    }

    // 
    public override void Initialize()
    {
        base.Initialize();

        // 注册相关观察者
        m_battleGame.RegisterGameEvent(ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverAchievement(this));
        m_battleGame.RegisterGameEvent(ENUM_GameEvent.SoldierKilled, new SoldierKilledObserverAchievement(this));
        m_battleGame.RegisterGameEvent(ENUM_GameEvent.NewStage, new NewStageObserverAchievement(this));
    }

    //增加Enemy阵亡数
    public void AddEnemyKilledCount()
    {
        m_EnemyKilledCount++;
    }

    // 增加Soldier阵亡数
    public void AddSoldierKilledCount()
    {
        m_SoldierKilledCount++;
    }

   /// <summary>
   /// 当前关卡
   /// </summary>
    /// <param name="NowStageLevel"></param>
    public void SetNowStageLevel(int NowStageLevel)
    {
        m_StageLv = NowStageLevel;
    }

    /// <summary>
    /// 产生存档
    /// </summary>
    /// <returns></returns>
    public AchievementSaveData CreateSaveData()
    {
        AchievementSaveData SaveData = new AchievementSaveData();

        // 设定新的高分
        SaveData.EnemyKilledCount = Mathf.Max(m_EnemyKilledCount, m_LastSaveData.EnemyKilledCount);
        SaveData.SoldierKilledCount = Mathf.Max(m_SoldierKilledCount, m_LastSaveData.SoldierKilledCount);
        SaveData.StageLv = Mathf.Max(m_StageLv, m_LastSaveData.StageLv);

        return SaveData;
    }

    //设定旧的存档
    public void SetSaveData(AchievementSaveData SaveData)
    {
        m_LastSaveData = SaveData;
    }

    // 储存记录
    /*public void SaveData()
	{
		PlayerPrefs.SetInt("EnemyKilledCount"	,m_EnemyKilledCount);
		PlayerPrefs.SetInt("SoldierKilledCount"	,m_SoldierKilledCount);
		PlayerPrefs.SetInt("StageLv"		 	,m_StageLv);
	}

	// 取回记录
	public void LoadData()
	{
		m_EnemyKilledCount 	= PlayerPrefs.GetInt("EnemyKilledCount",0);
		m_SoldierKilledCount= PlayerPrefs.GetInt("SoldierKilledCount",0);
		m_StageLv 			= PlayerPrefs.GetInt("StageLv",0);
	}*/


}
