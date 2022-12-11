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

    private PlayerSystem m_playerSystem;//���ϵͳ
    private NodeSystem m_nodeSystem;//�ڵ�ϵͳ
    private CameraSystem m_cameraSystem;//���ϵͳ

    private MoveUI m_moveUi;//�ж�����
    private HintUI m_hintUi;//��ʾ����
    private EndUI m_endUI;//��������

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
    /// ��ȡ��ǰ���
    /// </summary>
    /// <returns></returns>
    public IPlayer GetCurrentPlayer()
    {
       return m_playerSystem.CurrentPlayer;
    }
    /// <summary>
    /// ��ȡ�鿨����
    /// </summary>
    /// <returns></returns>
    public int GetDrawCardCount()
    {
        return m_moveUi.Count;
    }
    /// <summary>
    /// ��ȡһ��·��λ��
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetRoutePos(int start,int end)
    {
        return m_nodeSystem.GetRoutePos(start, end);
    }
    /// <summary>
    /// ������ʾ����
    /// </summary>
    public void SetHintContent(string content)
    {
        m_hintUi.SetContent(content);
    }
    /// <summary>
    /// ���ÿ���Ķ���
    /// </summary>
    /// <param name="transform"></param>
    public void SetLookAt(Transform transform)
    {
        m_cameraSystem.SetLookAt(transform);
    }
    /// <summary>
    /// ��ȡ�����±�
    /// </summary>
    /// <returns></returns>
    public List<int> GetTarpIndex()
    {
        return m_nodeSystem.GetTarpIndex();
    }
    /// <summary>
    /// ��ȡ�ڵ㳤��
    /// </summary>
    /// <returns></returns>
    public int GetNodesCount()
    {
        return m_nodeSystem.NodeCount;
    }
}
