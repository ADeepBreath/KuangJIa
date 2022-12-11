
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 兵营系统
/// </summary>
public class CampSystem:IGameSystem
{
    private Dictionary<ENUM_Soldier, ICamp> m_SoldierCamps = new Dictionary<ENUM_Soldier, ICamp>();
    private Dictionary<ENUM_Enemy, ICamp> m_CaptiveCamps = new Dictionary<ENUM_Enemy, ICamp>();

    public CampSystem(BattleGame battleGame) : base(battleGame)
    {
        Initialize();
    }

    /// <summary>
    /// 初始兵营系統
    /// </summary>
    public override void Initialize()
    {
        // 加入三个兵营
        m_SoldierCamps.Add(ENUM_Soldier.Rookie, SoldierCampFactory(ENUM_Soldier.Rookie));
        m_SoldierCamps.Add(ENUM_Soldier.Sergeant, SoldierCampFactory(ENUM_Soldier.Sergeant));
        m_SoldierCamps.Add(ENUM_Soldier.Captain, SoldierCampFactory(ENUM_Soldier.Captain));

        // 加入一个俘兵营
        m_CaptiveCamps.Add(ENUM_Enemy.Elf, CaptiveCampFactory(ENUM_Enemy.Elf));
        // 注册游戏事件观察者
        m_battleGame.RegisterGameEvent(ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverCaptiveCamp(this));
    }
    /// <summary>
    /// 更新
    /// </summary>
    public override void Update()
    {
        // 兵营执行命令
        foreach (SoldierCamp Camp in m_SoldierCamps.Values)
            Camp.RunCommand();
        foreach (CaptiveCamp Camp in m_CaptiveCamps.Values)
            Camp.RunCommand();
    }
    /// <summary>
    /// 获取场景中的兵营
    /// </summary>
    private SoldierCamp SoldierCampFactory(ENUM_Soldier emSoldier)
    {
        string GameObjectName = "SoldierCamp_";
        float CoolDown = 0;//冷却
        string CampName = "";
        string IconSprite = "";
        switch (emSoldier)
        {
            case ENUM_Soldier.Rookie:
                GameObjectName += "Rookie";
                CoolDown = 3;
                CampName = "菜鸟兵营";
                IconSprite = "RookieCamp";
                break;
            case ENUM_Soldier.Sergeant:
                GameObjectName += "Sergeant";
                CoolDown = 4;
                CampName = "中士兵营";
                IconSprite = "SergeantCamp";
                break;
            case ENUM_Soldier.Captain:
                GameObjectName += "Captain";
                CoolDown = 5;
                CampName = "上尉兵营";
                IconSprite = "CaptainCamp";
                break;
            default:
                Debug.Log("没有指定[" + emSoldier + "]要取得的场景物件名称！");
                break;
        }

        // 取得物件
        GameObject theGameObject = UnityTool.FindGameObject(GameObjectName);

        // 取得集合点
        Vector3 TrainPoint = GetTrainPoint(GameObjectName);

        // 产生兵营
        SoldierCamp NewCamp = new SoldierCamp(theGameObject, emSoldier, CampName, IconSprite, CoolDown, TrainPoint);
        NewCamp.SetBattleGame(m_battleGame);

        // 設定兵營使用的Script
        AddCampScript(theGameObject, NewCamp);

        return NewCamp;
    }

    /// <summary>
    /// 显示场景中的俘兵营
    /// </summary>
    public void ShowCaptiveCamp()
    {
        if (m_CaptiveCamps[ENUM_Enemy.Elf].GetVisible() == false)
        {
            m_CaptiveCamps[ENUM_Enemy.Elf].SetVisible(true);
            m_battleGame.ShowGameMsg("获得俘兵营");
        }
    }
    /// <summary>
    /// 获取场景中的俘兵营
    /// </summary>
    /// <param name="emEnemy"></param>
    /// <returns></returns>
    private CaptiveCamp CaptiveCampFactory(ENUM_Enemy emEnemy)
    {
        string GameObjectName = "CaptiveCamp_";
        float CoolDown = 0;
        string CampName = "";
        string IconSprite = "";
        switch (emEnemy)
        {
            case ENUM_Enemy.Elf:
                GameObjectName += "Elf";
                CoolDown = 3;
                CampName = "精灵俘兵营";
                IconSprite = "CaptiveCamp";
                break;
            default:
                Debug.Log("没有指定[" + emEnemy + "]要取得的场景物件名称！");
                break;
        }

        // 取得物件
        GameObject theGameObject = UnityTool.FindGameObject(GameObjectName);

        // 取得集合点
        Vector3 TrainPoint = GetTrainPoint(GameObjectName);

        //产生兵营
        CaptiveCamp NewCamp = new CaptiveCamp(theGameObject, emEnemy, CampName, IconSprite, CoolDown, TrainPoint);
        NewCamp.SetBattleGame(m_battleGame);

        // 设定兵营使用的Script
        AddCampScript(theGameObject, NewCamp);
        // 先隐藏
        NewCamp.SetVisible(false);

        return NewCamp;
    }
    /// <summary>
    /// 取得集合点
    /// </summary>
    /// <param name="GameObjectName"></param>
    /// <returns></returns>
    private Vector3 GetTrainPoint(string GameObjectName)
    {
        // 取得物件
        GameObject theCamp = UnityTool.FindGameObject(GameObjectName);
        // 取得集合点
        GameObject theTrainPoint = UnityTool.FindChildGameObject(theCamp, "TrainPoint");
        theTrainPoint.SetActive(false);

        return theTrainPoint.transform.position;
    }
    /// <summary>
    /// 设定兵营使用的Script
    /// </summary>
    /// <param name="theGameObject"></param>
    /// <param name="Camp"></param>
    private void AddCampScript(GameObject theGameObject, ICamp Camp)
    {
        // 加入Script
        CampOnClick CampScript = theGameObject.AddComponent<CampOnClick>();
        CampScript.theCamp = Camp;
    }
    /// <summary>
    /// 通知训练
    /// </summary>
    /// <param name="emSoldier"></param>
    public void UTTrainSoldier(ENUM_Soldier emSoldier)
    {
        if (m_SoldierCamps.ContainsKey(emSoldier))
            m_SoldierCamps[emSoldier].Train();
    }
}
