using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// AI状态基类
/// </summary>
public abstract class IAIState
{
    protected ICharacterAI m_CharacterAI = null; // 角色AI(状态的持有者）

    public IAIState()
    { }

    // 设定CharacterAI的对象
    public void SetCharacterAI(ICharacterAI CharacterAI)
    {
        m_CharacterAI = CharacterAI;
    }

    // 设定要攻击的目标
    public virtual void SetAttackPosition(Vector3 AttackPosition)
    { }

    // 更新
    public abstract void Update(List<ICharacter> Targets);

    //目标被移除
    public virtual void RemoveTarget(ICharacter Target)
    { }

}
