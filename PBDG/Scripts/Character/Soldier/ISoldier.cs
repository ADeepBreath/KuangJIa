﻿using UnityEngine;
using System.Collections;

// Soldier类型
public enum ENUM_Soldier
{
    Null = 0,
    Rookie = 1, // 新兵
    Sergeant = 2,   // 中士
    Captain = 3,    // 上尉
    Captive = 4,    // 俘兵
    Max,
}

/// <summary>
/// 士兵基类
/// </summary>
public abstract class ISoldier : ICharacter
{
    protected ENUM_Soldier m_emSoldier = ENUM_Soldier.Null;
    protected int m_MedalCount = 0;                 // 勋章数
    protected const int MAX_MEDAL = 3;                  // 最多勋章數 
    protected const int MEDAL_VALUE_ID = 20;                // 勋章數值起始值

    public ISoldier()
    {
    }

    public ENUM_Soldier GetSoldierType()
    {
        return m_emSoldier;
    }

    // 取得目前的角色值
    public SoldierAttr GetSoldierValue()
    {
        return m_Attribute as SoldierAttr;
    }

    // 被武器攻击
    public override void UnderAttack(ICharacter Attacker)
    {
        // 计算上海值
        m_Attribute.CalDmgValue(Attacker);

        // 是否阵亡
        if (m_Attribute.GetNowHP() <= 0)
        {
            DoPlayKilledSound();    // 音效
            DoShowKilledEffect();   // 特效 
            Killed();           // 阵亡
        }
    }

    // 增加勋章
    public virtual void AddMedal()
    {
        if (m_MedalCount >= MAX_MEDAL)
            return;

        // 增加勋章
        m_MedalCount++;
        // 取得对应的勋章加成值
        int AttrID = m_MedalCount + MEDAL_VALUE_ID;

        IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();

        // 加上字尾能力
        SoldierAttr SufAttr = theAttrFactory.GetEliteSoldierAttr(ENUM_AttrDecorator.Suffix, AttrID, m_Attribute as SoldierAttr);
        SetCharacterAttr(SufAttr);
    }

    // 执行Visitor
    public override void RunVisitor(ICharacterVisitor Visitor)
    {
        Visitor.VisitSoldier(this);
    }

    // 播放音效
    public abstract void DoPlayKilledSound();

    // 播放特效
    public abstract void DoShowKilledEffect();



}