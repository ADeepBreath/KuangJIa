using Cinemachine;
using UnityEngine;
/// <summary>
/// 相机系统
/// </summary>
public class CameraSystem : IGameSystem,IObserver
{
    CinemachineVirtualCamera playerCM,gameCM;
    public CameraSystem(Game game) : base(game)
    {
        Initialize();
    }

    public override void Initialize()
    {
        playerCM = UnityTool.FindGameObject("PlayerCM").GetComponent<CinemachineVirtualCamera>();
        gameCM= UnityTool.FindGameObject("GameCM").GetComponent<CinemachineVirtualCamera>();
        SetLookAt(m_Game.GetCurrentPlayer().GetCurrentObj().transform);

        MessageCenter.Instance.RegisterObservers(MessageID.SwitchoverCurrentPlayer, this);//注册玩家移动消息
        MessageCenter.Instance.RegisterObservers(MessageID.TurnOrgan, this);//注册转动机关消息
    }
    public override void Release()
    {
        playerCM = null;
        gameCM = null;

        MessageCenter.Instance.RemoveObservers(MessageID.SwitchoverCurrentPlayer, this);
        MessageCenter.Instance.RemoveObservers(MessageID.TurnOrgan, this);
    }
    public void HandleNotification(MessageID id)
    {
        switch (id)
        {
            case MessageID.SwitchoverCurrentPlayer:
                {
                    SetLookAt(m_Game.GetCurrentPlayer().GetCurrentObj().transform);
                }
                break;
            case MessageID.TurnOrgan:
                {
                    SelectGameCM();
                }
                break;
        }
    }
    /// <summary>
    /// 设置看向对象
    /// </summary>
    public void SetLookAt(Transform go)
    {
        if(!playerCM.gameObject.activeSelf)
        {
            playerCM.gameObject.SetActive(true);
        }
        playerCM.LookAt = go;
        playerCM.Follow = go;
    }
    /// <summary>
    /// 选择游戏相机
    /// </summary>
    public void SelectGameCM()
    {
        if (playerCM.gameObject.activeSelf)
        {
            playerCM.gameObject.SetActive(false);
        }
    }

}
