using UnityEngine;
using System.Collections;

/// <summary>
/// 成就记录存档
/// </summary>
public class AchievementSaveData
{
    // 成就要存档的信息
    public int EnemyKilledCount { get; set; }
    public int SoldierKilledCount { get; set; }
    public int StageLv { get; set; }

    public AchievementSaveData() { }

    public void SaveData()
    {
        PlayerPrefs.SetInt("EnemyKilledCount", EnemyKilledCount);
        PlayerPrefs.SetInt("SoldierKilledCount", SoldierKilledCount);
        PlayerPrefs.SetInt("StageLv", StageLv);
    }

    public void LoadData()
    {
        EnemyKilledCount = PlayerPrefs.GetInt("EnemyKilledCount", 0);
        SoldierKilledCount = PlayerPrefs.GetInt("SoldierKilledCount", 0);
        StageLv = PlayerPrefs.GetInt("StageLv", 0);
    }
}
