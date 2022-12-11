using UnityEngine;
using System.Collections;

/// <summary>
/// 敌方俘兵
/// </summary>
public class EnemyCaptive : IEnemy
{
    private ISoldier m_Captive = null;

    // 
    public EnemyCaptive(ISoldier theSoldier, Vector3 AttackPos)
    {
        m_emEnemyType = ENUM_Enemy.Catpive;
        m_Captive = theSoldier;

        // 设定对象
        SetGameObject(theSoldier.GetGameObject());

        // 将Soldier數值传成Enemy用的
        EnemyAttr tempAttr = new EnemyAttr();
        SetCharacterAttr(tempAttr);

        // 设定武器
        SetWeapon(theSoldier.GetWeapon());

        // 更改为SoldierAI
        m_AI = new EnemyAI(this, AttackPos);
        m_AI.ChangeAIState(new IdleAIState());
    }

    // 播放音效
    public override void DoPlayHitSound()
    {
        m_Captive.DoPlayKilledSound();
    }

    // 播放特效
    public override void DoShowHitEffect()
    {
        m_Captive.DoShowKilledEffect();
    }

    // 执行Visitor
    public override void RunVisitor(ICharacterVisitor Visitor)
    {
        //
    }

}
