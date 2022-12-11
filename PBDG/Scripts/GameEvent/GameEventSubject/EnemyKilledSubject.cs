﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 敌人单位阵亡
/// </summary>
public class EnemyKilledSubject : IGameEventSubject
{
    private int m_KilledCount = 0;
    private IEnemy m_Enemy = null;

    public EnemyKilledSubject()
    { }

    //取得对象
    public IEnemy GetEnemy()
    {
        return m_Enemy;
    }

    // 目前敌人单位阵亡数
    public int GetKilledCount()
    {
        return m_KilledCount;
    }

    // 通通知敌人单位阵亡
    public override void SetParam(System.Object Param)
    {
        base.SetParam(Param);
        m_Enemy = Param as IEnemy;
        m_KilledCount++;

        // 通知
        Notify();
    }
}
