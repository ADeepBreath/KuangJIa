
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class BattleGame
{
    private static BattleGame _instance;
    public static BattleGame Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new BattleGame();
            }
            return _instance;
        }
    }

    //场景状态控制
    private bool m_bGameOver = false;
    //游戏系统
    private GameEventSystem m_GameEventSystem = null;//游戏事件系统
    private CampSystem m_CampSystem = null;         //兵营系统
    private StageSystem m_StageSystem = null;            //关卡系统
    private CharacterSystem m_CharacterSystem = null;    // 角色管理系統
    private APSystem m_ApSystem = null;                  // 行动力系統
    private AchievementSystem m_AchievementSystem = null;// 成就系統

    // 界面
    private CampInfoUI m_CampInfoUI = null;              // 兵营界面
    private SoldierInfoUI m_SoldierInfoUI = null;		 // 战士信息界面
    private GameStateInfoUI m_GameStateInfoUI = null;    // 游戏状态界面
    private GamePauseUI m_GamePauseUI = null;			 // 游戏暂停界面

    private BattleGame()
    {
        
    }

    /// <summary>
    /// 初始BattleGame游戏相关设定
    /// </summary>
    public void Initinal()
    {
        // 场景状态控制
        m_bGameOver = false;
        // 遊戲系統
        m_GameEventSystem = new GameEventSystem(this);  // 游戏事件系统
        m_CampSystem = new CampSystem(this);            // 兵营系统
        m_StageSystem = new StageSystem(this);          //关卡系统
        m_CharacterSystem = new CharacterSystem(this);  // 角色管理系統
        m_ApSystem = new APSystem(this);                // 行动力系統
        m_AchievementSystem = new AchievementSystem(this); // 成就系統
         // 界面
        m_CampInfoUI = new CampInfoUI(this);            // 兵营界面
        m_SoldierInfoUI = new SoldierInfoUI(this);      // 战士信息界面								
        m_GameStateInfoUI = new GameStateInfoUI(this);  // 游戏状态界面
        m_GamePauseUI = new GamePauseUI(this);          // 游戏暂停界面

        // 注入到其它系统
        EnemyAI.SetStageSystem(m_StageSystem);

        //载入存档
        LoadData();

        // 注册游戏事件系統
        ResigerGameEvent();
    }
    /// <summary>
    /// 注册游戏事件系统
    /// </summary>
    private void ResigerGameEvent()
    {
        // 事件注册
        m_GameEventSystem.RegisterObserver(ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverUI(this));
    }
    /// <summary>
    /// 释放游戏系统
    /// </summary>
    public void Release()
    {
        // 遊戲系統
        m_GameEventSystem.Release();
        m_CampSystem.Release();
        m_StageSystem.Release();
        m_CharacterSystem.Release();
        m_ApSystem.Release();
        m_AchievementSystem.Release();
        // 界面
        m_CampInfoUI.Release();
        m_SoldierInfoUI.Release();
        m_GameStateInfoUI.Release();
        m_GamePauseUI.Release();
        UITool.ReleaseCanvas();

        // 存档
        SaveData();
    }
    /// <summary>
    /// 游戏更新
    /// </summary>
    public void Update()
    {
        // 玩家輸入
        InputProcess();

        // 游戏系统更新
        m_GameEventSystem.Update();
        m_CampSystem.Update();
        m_StageSystem.Update();
        m_CharacterSystem.Update();
        m_ApSystem.Update();
        m_AchievementSystem.Update();

        // 玩家界面更新
        m_CampInfoUI.Update();
        m_SoldierInfoUI.Update();
        m_GameStateInfoUI.Update();
        m_GamePauseUI.Update();
    }
    /// <summary>
    ///  玩家输入
    /// </summary>
    private void InputProcess()
    {
        //  Mouse左鍵
        if (Input.GetMouseButtonUp(0) == false)
            return;

        //由相机产生一条射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        // 走访每一个被Hit到的GameObject
        foreach (RaycastHit hit in hits)
        {
            // 是否有兵營点击
            CampOnClick CampClickScript = hit.transform.gameObject.GetComponent<CampOnClick>();
            if (CampClickScript != null)
            {
                CampClickScript.OnClick();
                return;
            }

            // 是否有角色点击
            SoldierOnClick SoldierClickScript = hit.transform.gameObject.GetComponent<SoldierOnClick>();
            if (SoldierClickScript != null)
            {
                SoldierClickScript.OnClick();
                return;
            }
        }
    }
    /// <summary>
    /// 游戏状态
    /// </summary>
    /// <returns></returns>
    public bool ThisGameIsOver()
    {
        return m_bGameOver;
    }
    /// <summary>
    /// 返回主菜单
    /// </summary>
    public void ChangeToMainMenu()
    {
        m_bGameOver = true;
    }
    /// <summary>
    /// 游戏结束
    /// </summary>
    public void OnGameOver()
    {
        m_bGameOver = true;
    }
    /// <summary>
    ///  增加Soldier
    /// </summary>
    /// <param name="theSoldier"></param>
    public void AddSoldier(ISoldier theSoldier)
    {
        if (m_CharacterSystem != null)
            m_CharacterSystem.AddSoldier(theSoldier);
    }
    /// <summary>
    ///  移除Soldier
    /// </summary>
    /// <param name="theSoldier"></param>
    public void RemoveSoldier(ISoldier theSoldier)
    {
        if (m_CharacterSystem != null)
            m_CharacterSystem.RemoveSoldier(theSoldier);
    }
    /// <summary>
    ///  增加Enemy
    /// </summary>
    /// <param name="theEnemy"></param>
    public void AddEnemy(IEnemy theEnemy)
    {
        if (m_CharacterSystem != null)
            m_CharacterSystem.AddEnemy(theEnemy);
    }

    /// <summary>
    ///  移除Enemy
    /// </summary>
    /// <param name="theEnemy"></param>
    public void RemoveEnemy(IEnemy theEnemy)
    {
        if (m_CharacterSystem != null)
            m_CharacterSystem.RemoveEnemy(theEnemy);
    }

    /// <summary>
    /// 目前敌人数量
    /// </summary>
    /// <returns></returns>
    public int GetEnemyCount()
    {
        if (m_CharacterSystem != null)
            return m_CharacterSystem.GetEnemyCount();
        return 0;
    }

    /// <summary>
    ///  增加敌人阵亡數量(不透过GameEventSystem呼叫) 
    /// </summary>
    public void AddEnemyKilledCount()
    {
        m_StageSystem.AddEnemyKilledCount();
    }
    /// <summary>
    ///  执行角色系統的Visitor
    /// </summary>
    /// <param name="Visitor"></param>
    public void RunCharacterVisitor(ICharacterVisitor Visitor)
    {
        m_CharacterSystem.RunVisitor(Visitor);
    }

    /// <summary>
    /// 注册游戏事件
    /// </summary>
    public void RegisterGameEvent(ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
    {
        m_GameEventSystem.RegisterObserver(emGameEvent, Observer);
    }
    /// <summary>
    ///  通知游戏事件
    /// </summary>
    /// <param name="emGameEvent"></param>
    /// <param name="Param"></param>
    public void NotifyGameEvent(ENUM_GameEvent emGameEvent, System.Object Param)
    {
        m_GameEventSystem.NotifySubject(emGameEvent, Param);
    }
    /// <summary>
    /// 显示兵营信息
    /// </summary>
    /// <param name="Camp"></param>
    public void ShowCampInfo(ICamp Camp)
    {
        m_CampInfoUI.ShowInfo(Camp);
        m_SoldierInfoUI.Hide();
    }
    /// <summary>
    ///  显示Soldier信息
    /// </summary>
    /// <param name="Soldier"></param>
    public void ShowSoldierInfo(ISoldier Soldier)
    {
        m_SoldierInfoUI.ShowInfo(Soldier);
        m_CampInfoUI.Hide();
    }

    /// <summary>
    /// 通知AP更新
    /// </summary>
    /// <param name="NowAP"></param>
    public void APChange(int NowAP)
    {
        m_GameStateInfoUI.ShowAP(NowAP);
    }
    /// <summary>
    ///  花费AP
    /// </summary>
    /// <param name="ApValue"></param>
    /// <returns></returns>
    public bool CostAP(int ApValue)
    {
        return m_ApSystem.CostAP(ApValue);
    }

    /// <summary>
    ///  显示关卡
    /// </summary>
    /// <param name="Lv"></param>
    public void ShowNowStageLv(int Lv)
    {
        m_GameStateInfoUI.ShowNowStageLv(Lv);
    }
    /// <summary>
    /// 游戏信息
    /// </summary>
    /// <param name="Msg"></param>
    public void ShowGameMsg(string Msg)
    {
        m_GameStateInfoUI.ShowMsg(Msg);
    }
    /// <summary>
    ///  显示Heart
    /// </summary>
    /// <param name="Value"></param>
    public void ShowHeart(int Value)
    {
        m_GameStateInfoUI.ShowHeart(Value);
        ShowGameMsg("陣營被攻擊");
    }

    /// <summary>
    /// 显示暂停
    /// </summary>
    public void GamePause()
    {
        if (m_GamePauseUI.IsVisible() == false)
            m_GamePauseUI.ShowGamePause(m_AchievementSystem.CreateSaveData());
        else
            m_GamePauseUI.Hide();
    }

    /// <summary>
    /// 存档
    /// </summary>
    private void SaveData()
    {
        AchievementSaveData SaveData = m_AchievementSystem.CreateSaveData();
        SaveData.SaveData();
    }

    /// <summary>
    ///  取回存档
    /// </summary>
    /// <returns></returns>
    private AchievementSaveData LoadData()
    {
        AchievementSaveData OldData = new AchievementSaveData();
        OldData.LoadData();
        m_AchievementSystem.SetSaveData(OldData);
        return OldData;
    }

    /*#region 直接取得角色數量的實作方式
	// 目前Soldier數量
	public int GetSoldierCount()
	{
		if( m_CharacterSystem !=null)
			return m_CharacterSystem.GetSoldierCount();
		return 0;
	}

	// 目前Soldier數量
	public int GetSoldierCount( ENUM_Soldier emSoldier)
	{
		if( m_CharacterSystem !=null)
			return m_CharacterSystem.GetSoldierCount(emSoldier);
		return 0;
	}	
	#endregion*/
}
