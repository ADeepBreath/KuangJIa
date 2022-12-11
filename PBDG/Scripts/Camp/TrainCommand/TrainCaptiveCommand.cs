using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 训练俘兵
/// </summary>
public class TrainCaptiveCommand : ITrainCommand
{

    private BattleGame m_battleGame = null;
    private ENUM_Enemy m_emEnemy; // 兵种
    private Vector3 m_Position; // 出現位置

    public TrainCaptiveCommand(ENUM_Enemy emEnemy, Vector3 Position, BattleGame battleGame)
    {
        m_battleGame = battleGame;
        m_emEnemy = emEnemy;
        m_Position = Position;
    }

    public override void Execute()
    {
        // 先产生Enemy
        ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
        IEnemy theEnemy = Factory.CreateEnemy(m_emEnemy, ENUM_Weapon.Gun, m_Position, Vector3.zero);

        // 再建立俘兵(轉接器)
        SoldierCaptive NewSoldier = new SoldierCaptive(theEnemy);

        // 移除Enemy
        m_battleGame.RemoveEnemy(theEnemy);

        // 加入Soldier
        m_battleGame.AddSoldier(NewSoldier);
    }
}
