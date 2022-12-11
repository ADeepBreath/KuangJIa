using UnityEngine;

/// <summary>
/// 俘兵兵营
/// </summary>
public class CaptiveCamp : ICamp
{
    private GameObject m_GameObject = null;
    private ENUM_Enemy m_emEnemy = ENUM_Enemy.Null;
    private Vector3 m_Position;

    /// <summary>
    /// 设定兵营产出的单位及冷却时间
    /// </summary>
    public CaptiveCamp(GameObject theGameObject, ENUM_Enemy emEnemy, string CampName, string IconSprite, float TrainCoolDown, Vector3 Position) : base(theGameObject, TrainCoolDown, CampName, IconSprite)
    {
        m_emSoldier = ENUM_Soldier.Captive;
        m_emEnemy = emEnemy;
        m_Position = Position;
    }

    /// <summary>
    ///  取得训练金额
    /// </summary>
    /// <returns></returns>
    public override int GetTrainCost()
    {
        return 10;
    }

    /// <summary>
    /// 训练Soldier
    /// </summary>
    public override void Train()
    {
        //生成一个训练命令
        TrainCaptiveCommand NewCommand = new TrainCaptiveCommand(m_emEnemy, m_Position, m_battleGame);
        AddTrainCommand(NewCommand);
    }
}
