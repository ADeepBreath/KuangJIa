using UnityEngine;
using System.Collections;

/// <summary>
/// 增加Solider勋章
/// </summary>
public class SoldierAddMedalVisitor : ICharacterVisitor
{
    BattleGame m_battleGame = null;

    public SoldierAddMedalVisitor(BattleGame battleGame)
    {
        m_battleGame = battleGame;
    }

    public override void VisitSoldier(ISoldier Soldier)
    {
        base.VisitSoldier(Soldier);
        Soldier.AddMedal();

        // 游戏事件
        m_battleGame.NotifyGameEvent(ENUM_GameEvent.SoldierUpgate, Soldier);
    }
}
