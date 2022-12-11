using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game
{
    private static Game _instance;
    public static Game Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Game();
            return _instance;
        }
    }

    private PlayerSystem m_playerSystem;//玩家系统
    private NodeSystem m_nodeSystem;//节点系统
    private CameraSystem m_cameraSystem;//相机系统

    private MoveUI m_moveUi;//行动界面
    private HintUI m_hintUi;//提示界面
    private EndUI m_endUI;//结束界面

    public void Initinal()
    {
        m_playerSystem = new PlayerSystem(this);
        m_nodeSystem = new NodeSystem(this);
        m_cameraSystem=new CameraSystem(this);

        m_moveUi = new MoveUI(this);
        m_hintUi=new HintUI(this);
        m_endUI=new EndUI(this);
    }

    public void Update()
    {
        m_playerSystem.Update();
        m_nodeSystem.Update();
        m_cameraSystem.Update();

        m_moveUi.Update();
        m_hintUi.Update();
        m_endUI.Update();


    }
    public void Release()
    {
        m_playerSystem.Release();
        m_nodeSystem.Release();
        m_cameraSystem.Release();

        m_moveUi.Release();
        m_hintUi.Release();
        m_endUI.Release();
    }
    /// <summary>
    /// 获取当前玩家
    /// </summary>
    /// <returns></returns>
    public IPlayer GetCurrentPlayer()
    {
       return m_playerSystem.CurrentPlayer;
    }
    /// <summary>
    /// 获取抽卡点数
    /// </summary>
    /// <returns></returns>
    public int GetDrawCardCount()
    {
        return m_moveUi.Count;
    }
    /// <summary>
    /// 获取一段路径位置
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetRoutePos(int start,int end)
    {
        return m_nodeSystem.GetRoutePos(start, end);
    }
    /// <summary>
    /// 设置提示内容
    /// </summary>
    public void SetHintContent(string content)
    {
        m_hintUi.SetContent(content);
    }
    /// <summary>
    /// 设置看向的对象
    /// </summary>
    /// <param name="transform"></param>
    public void SetLookAt(Transform transform)
    {
        m_cameraSystem.SetLookAt(transform);
    }
    /// <summary>
    /// 获取陷阱下标
    /// </summary>
    /// <returns></returns>
    public List<int> GetTarpIndex()
    {
        return m_nodeSystem.GetTarpIndex();
    }
    /// <summary>
    /// 获取节点长度
    /// </summary>
    /// <returns></returns>
    public int GetNodesCount()
    {
        return m_nodeSystem.NodeCount;
    }
}
