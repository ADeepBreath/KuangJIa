using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 玩家系统
/// </summary>
public class PlayerSystem : IGameSystem,IObserver
{
    IPlayer hostPlayer, otherPlayer;
    IPlayer currentPlayer;//当前玩家

    public IPlayer CurrentPlayer { get=>currentPlayer;}

    public PlayerSystem(Game game) : base(game)
    {
        Initialize();
    }

    public override void Initialize()
    {
        CreatePlayer();

        MessageCenter.Instance.RegisterObservers(MessageID.PlayerMove, this);
        MessageCenter.Instance.RegisterObservers(MessageID.SwitchoverCurrentPlayer, this);
        MessageCenter.Instance.RegisterObservers(MessageID.IsTriggerTarp, this);
        MessageCenter.Instance.RegisterObservers(MessageID.IsEnd, this);
    }
    public override void Release()
    {
        currentPlayer = null;
        hostPlayer.Release();
        otherPlayer.Release();
        hostPlayer=null;
        otherPlayer= null;

        MessageCenter.Instance.RemoveObservers(MessageID.PlayerMove, this);
        MessageCenter.Instance.RemoveObservers(MessageID.SwitchoverCurrentPlayer, this);
        MessageCenter.Instance.RemoveObservers(MessageID.IsTriggerTarp, this);
        MessageCenter.Instance.RemoveObservers(MessageID.IsEnd, this);
    }
    public void HandleNotification(MessageID id)
    {
        switch (id)
        {
            case MessageID.PlayerMove:
                {
                    int start = currentPlayer.CurrentNodeIndex;
                    int end = start + m_Game.GetDrawCardCount();
                    currentPlayer.SetMoveTargets(m_Game.GetRoutePos(start, end));
                }break;
            case MessageID.SwitchoverCurrentPlayer:
                {
                    SwitchoverCurrentPlayer();
                }
                break;
            case MessageID.IsTriggerTarp:
                {
                    IsTriggerTarp();
                }
                break;
            case MessageID.IsEnd:
                {
                    IsEnd();
                }
                break;
        }
    }
    float aiTimer = 0;
    public override void Update()
    {
        if (currentPlayer != null)
        {
            currentPlayer.Update();
        }

        if(aiTimer>0)
        {
            aiTimer -= Time.deltaTime;
            if(aiTimer<=0)
            {
                AIMove();
            }
        }
    }
    /// <summary>
    /// 是否到达终点
    /// </summary>
    public void IsEnd()
    {
        if(currentPlayer.CurrentNodeIndex==m_Game.GetNodesCount()-1)
        {
            if (currentPlayer is HostPlayer)
            {
                MessageCenter.Instance.NotifyObserver(MessageID.Win);
            }
            else
            {
                MessageCenter.Instance.NotifyObserver(MessageID.Lose);
            }
        }
    }
    /// <summary>
    /// 是否触发陷阱
    /// </summary>
    public void IsTriggerTarp()
    {
        List<int> tarpIndex = m_Game.GetTarpIndex();
        for (int i = 0; i < tarpIndex.Count; i++)
        {
            if (hostPlayer.CurrentNodeIndex == tarpIndex[i])
            {
                TriggerTarp(hostPlayer);
            }
            if(otherPlayer.CurrentNodeIndex == tarpIndex[i])
            {
                TriggerTarp(otherPlayer);
            }
        }
    }
    /// <summary>
    /// 触发陷阱
    /// </summary>
    public void TriggerTarp(IPlayer player)
    {
        player.DropOut();
        player.CutObj();
    }
    /// <summary>
    /// 生成玩家
    /// </summary>
    public void CreatePlayer()
    {
        hostPlayer = new HostPlayer("Role/1");
        otherPlayer = new OtherPlayer("Role/2");

        currentPlayer = hostPlayer;
    }
    /// <summary>
    /// 切换当前玩家
    /// </summary>
    public void SwitchoverCurrentPlayer()
    {
        if(currentPlayer is HostPlayer)
        {
            if(currentPlayer.CurrentNodeIndex!=-1)
            {
                if (currentPlayer.CurrentNodeIndex == otherPlayer.CurrentNodeIndex)
                {
                    int start = currentPlayer.CurrentNodeIndex;
                    int end = start + 1;
                    currentPlayer.SetMoveTargets(m_Game.GetRoutePos(start, end));
                    m_Game.SetHintContent("你额外行动1步！");
                    return;
                }
            }
            currentPlayer = otherPlayer;
            aiTimer = 1.5f;
        }
        else
        {
            if (currentPlayer.CurrentNodeIndex != -1)
            {
                if (currentPlayer.CurrentNodeIndex == hostPlayer.CurrentNodeIndex)
                {
                    int start = currentPlayer.CurrentNodeIndex;
                    int end = start + 1;
                    currentPlayer.SetMoveTargets(m_Game.GetRoutePos(start, end));
                    m_Game.SetHintContent("对方额外行动1步！");
                    return;
                }
            }
            currentPlayer = hostPlayer;
            MessageCenter.Instance.NotifyObserver(MessageID.ShowMovePanel);
        }
    }
    /// <summary>
    /// 自动移动
    /// </summary>
    public void AIMove()
    {
        int count = Random.Range(1, 8) / 2;
        if (count > 0)
        {
            int start = currentPlayer.CurrentNodeIndex;
            int end = start + count;
            currentPlayer.SetMoveTargets(m_Game.GetRoutePos(start, end));
            m_Game.SetLookAt(currentPlayer.GetCurrentObj().transform);
            m_Game.SetHintContent("对方行动" + count + "步！");
        }
        else
        {
            MessageCenter.Instance.NotifyObserver(MessageID.TurnOrgan);
            m_Game.SetHintContent("对方触发了机关！");
        }
    }


}
