
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 玩家基类
/// </summary>
public class IPlayer
{
    protected string prefab;

    protected List<GameObject> playerObjs=new List<GameObject> ();

    protected NavMeshAgent playerNav;
    protected Animator playerAni;
    protected int currentNodeIndex = -1;//当前所在格子
    protected List<Vector3> targets = new List<Vector3>();//目标路径

    public int CurrentNodeIndex { get => currentNodeIndex; set => currentNodeIndex = value; }

    public IPlayer(string prefab)
    {
        this.prefab = prefab;

        Init ();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public virtual void Init()
    {
        CreateObj();
    }
    public virtual void Update()
    {
        Move();
    }
    public  virtual void Release()
    {
        prefab = null;
        for (int i = 0; i < playerObjs.Count; i++)
        {
            GameObject.Destroy(playerObjs[0]);
        }
        playerObjs.Clear();
        GameObject.Destroy(playerNav);
        GameObject.Destroy(playerAni);
        targets.Clear();
    }
    /// <summary>
    /// 生成模型
    /// </summary>
    public void CreateObj()
    {
        //生成模型
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(prefab));
            obj.name = prefab + "_" + i + 1;
            playerObjs.Add(obj);
        }

        playerAni = playerObjs[0].GetComponent<Animator>();
    }
    /// <summary>
    /// 设置移动路径
    /// </summary>
    public void SetMoveTargets(List<Vector3> targets)
    {
        this.targets = targets;
    }
    /// <summary>
    /// 移动
    /// </summary>
    public void Move()
    {
        if(targets.Count>0)
        {
            playerNav.SetDestination(targets[0]);
            playerAni.SetBool("Move",true);
            if (!playerNav.pathPending && playerNav.remainingDistance < 1f)
            {
                currentNodeIndex++;
                MessageCenter.Instance.NotifyObserver(MessageID.IsTriggerTarp);//是否触发陷阱
                if(targets.Count>0)
                {
                    targets.RemoveAt(0);
                }
            }
            if(targets.Count<=0)
            {
                if(playerObjs.Count>0)
                {
                    playerNav.SetDestination(playerObjs[0].transform.position);
                    playerAni.SetBool("Move", false);
                }
                //切换当前玩家
                MessageCenter.Instance.NotifyObserver(MessageID.IsEnd);//是否到达终点
                MessageCenter.Instance.NotifyObserver(MessageID.SwitchoverCurrentPlayer);
            }
        }     
    }
    /// <summary>
    /// 掉落
    /// </summary>
    public void DropOut()
    {
        playerObjs[0].AddComponent<Rigidbody>();
        GameObject.Destroy(playerNav);
    }
    /// <summary>
    /// 切换模型
    /// </summary>
    public void CutObj()
    {
        targets.Clear();
        currentNodeIndex = -1;
        GameObject.Destroy(playerObjs[0],2f);
        playerObjs.RemoveAt(0);
        if(playerObjs.Count>0)
        {
            playerNav = playerObjs[0].AddComponent<NavMeshAgent>();
            playerAni = playerObjs[0].GetComponent<Animator>();
        }
        else
        {
            if(this is HostPlayer)
            {
                MessageCenter.Instance.NotifyObserver(MessageID.Lose);
            }
            else
            {
                MessageCenter.Instance.NotifyObserver(MessageID.Win);
            }
        }

    }
    /// <summary>
    /// 获取当前模型
    /// </summary>
    public GameObject GetCurrentObj()
    {
        if(playerObjs.Count<=0)
        {
            Debug.Log("当前生命已用尽！");
            return null;
        }
        return playerObjs[0];
    }
}
