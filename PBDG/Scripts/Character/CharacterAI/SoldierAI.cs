using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 玩家角色AI
/// </summary>
public class SoldierAI : ICharacterAI
{
    public SoldierAI(ICharacter Character) : base(Character)
    {
        // 一开始的起始状态
        ChangeAIState(new IdleAIState());
    }

    // 是否可以攻击Heart
    public override bool CanAttackHeart()
    {
        return false;
    }
}

