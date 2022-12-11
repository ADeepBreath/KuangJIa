using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 兵营界面
/// </summary>
public abstract class ICamp
{
    protected GameObject m_GameObject = null;
    protected string m_Name = "Null"; //名称
    protected string m_IconSpriteName = "";
    protected ENUM_Soldier m_emSoldier = ENUM_Soldier.Null;

    // 训练命令
    protected List<ITrainCommand> m_TrainCommands = new List<ITrainCommand>();
    protected float m_CommandTimer = 0;  // 目前冷却剩余时间
    protected float m_TrainCoolDown = 0; // 冷却时间

    // 训练花费
    protected ITrainCost m_TrainCost = null;

    // 主游戏界面（必要时设定）
    protected BattleGame m_battleGame = null;

    public ICamp(GameObject GameObj, float TrainCoolDown, string Name, string IconSprite)
    {
        m_GameObject = GameObj;
        m_TrainCoolDown = TrainCoolDown;
        m_CommandTimer = m_TrainCoolDown;
        m_Name = Name;
        m_IconSpriteName = IconSprite;
        m_TrainCost = new TrainCost();
    }

    /// <summary>
    /// 设定主游戏界面
    /// </summary>
    /// <param name="battleGame"></param>
    public void SetBattleGame(BattleGame battleGame)
    {
        m_battleGame = battleGame;
    }

    /// <summary>
    /// 目前
    /// </summary>
    /// <returns></returns>
    public ENUM_Soldier GetSoldierType()
    {
        return m_emSoldier;
    }
    /// <summary>
    ///  新增训练命令
    /// </summary>
    /// <param name="Command"></param>
    protected void AddTrainCommand(ITrainCommand Command)
    {
        m_TrainCommands.Add(Command);
    }
    /// <summary>
    /// 删除训练命令
    /// </summary>
    public void RemoveLastTrainCommand()
    {
        if (m_TrainCommands.Count == 0)
            return;
        // 移除最后一个
        m_TrainCommands.RemoveAt(m_TrainCommands.Count - 1);
    }
    /// <summary>
    /// 目前训练命令数量
    /// </summary>
    /// <returns></returns>
    public int GetTrainCommandCount()
    {
        return m_TrainCommands.Count;
    }
    /// <summary>
    /// 执行命令
    /// </summary>
    public void RunCommand()
    {
        //没有命令不执行
        if (m_TrainCommands.Count == 0)
            return;

        // 冷却结束
        m_CommandTimer -= Time.deltaTime;
        if (m_CommandTimer > 0)
            return;
        m_CommandTimer = m_TrainCoolDown;

        //执行第一个命令
        m_TrainCommands[0].Execute();

        // 移除
        m_TrainCommands.RemoveAt(0);
    }

    /// <summary>
    /// 获取等级
    /// </summary>
    /// <returns></returns>
    public virtual int GetLevel()
    {
        return 0;
    }
    /// <summary>
    /// 升级花费
    /// </summary>
    /// <returns></returns>
    public virtual int GetLevelUpCost() { return 0; }

    /// <summary>
    /// 升级
    /// </summary>
    public virtual void LevelUp() { }

    /// <summary>
    /// 武器等级
    /// </summary>
    /// <returns></returns>
    public virtual ENUM_Weapon GetWeaponType()
    {
        return ENUM_Weapon.Null;
    }

    /// <summary>
    ///  武器升级花费
    /// </summary>
    /// <returns></returns>
    public virtual int GetWeaponLevelUpCost() { return 0; }

    /// <summary>
    /// 武器升级
    /// </summary>
    public virtual void WeaponLevelUp() { }

    /// <summary>
    /// 训练数
    /// </summary>
    /// <returns></returns>
    public int GetOnTrainCount()
    {
        return m_TrainCommands.Count;
    }

    /// <summary>
    /// 训练时间
    /// </summary>
    /// <returns></returns>
    public float GetTrainTimer()
    {
        return m_CommandTimer;
    }

    /// <summary>
    /// 名称
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return m_Name;
    }

    /// <summary>
    ///  图标名
    /// </summary>
    /// <returns></returns>
    public string GetIconSpriteName()
    {
        return m_IconSpriteName;
    }

    /// <summary>
    /// 写入显示状态
    /// </summary>
    /// <param name="bValue"></param>
    public void SetVisible(bool bValue)
    {
        m_GameObject.SetActive(bValue);
    }

    /// <summary>
    /// 获取显示状态
    /// </summary>
    /// <returns></returns>
    public bool GetVisible()
    {
        return m_GameObject.activeSelf;
    }

    /// <summary>
    /// 获取训练金额
    /// </summary>
    /// <returns></returns>
    public abstract int GetTrainCost();

    /// <summary>
    /// 训练
    /// </summary>
    public abstract void Train();
}