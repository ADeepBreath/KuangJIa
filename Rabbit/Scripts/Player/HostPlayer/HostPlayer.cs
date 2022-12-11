using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 自己
/// </summary>
public class HostPlayer : IPlayer
{
    public HostPlayer(string prefab) : base(prefab)
    {

    }

    public override void Init()
    {
        base.Init();
        SetStartPos();      
    }
    /// <summary>
    /// 设置初始地点
    /// </summary>
    public void SetStartPos()
    {
        GameObject playerStart = UnityTool.FindGameObject("PlayerStart");
        for (int i = 0; i < playerObjs.Count; i++)
        {
            playerObjs[i].transform.position = playerStart.transform.GetChild(i).position;
        }

        playerNav = playerObjs[0].AddComponent<NavMeshAgent>();
    }
}
