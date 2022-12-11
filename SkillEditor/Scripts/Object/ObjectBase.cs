using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ObjectBase
{
    public GameObject m_go;//持有模型
    public ObjectInfoBase m_info;//持有数据

    //组件
    public Animator m_animator;
    public NavMeshAgent m_navMeshAgent;


    public ObjectBase(ObjectInfoBase info)
    {
        m_info = info;
        CreateObj();
    }

    /// <summary>
    /// 创建模型
    /// </summary>
    public virtual void CreateObj()
    {
        m_go=GameObject.Instantiate(Resources.Load<GameObject>(m_info.modelPath));
        m_go.name=m_info.name;
        m_go.transform.position=new Vector3(m_info.position[0],m_info.position[1],m_info.position[2]);
        m_go.transform.eulerAngles=new Vector3(m_info.angle[0],m_info.angle[1],m_info.angle[2]);
        if(m_go)
        {
            OnCreate();
        }
    }

    /// <summary>
    /// 当创建模型时初始逻辑
    /// </summary>
    public virtual void OnCreate()
    {
        m_animator = m_go.GetComponent<Animator>();
        m_navMeshAgent= m_go.AddComponent<NavMeshAgent>();
        m_go.AddComponent<LocalNavMeshBuilder>();
    }
    /// <summary>
    /// 设置位置
    /// </summary>
    /// <param name="pos"></param>
    public void SetPos(Vector3 pos)
    {
        m_go.transform.position = pos;
    }
    /// <summary>
    /// 导航到目的地
    /// </summary>
    /// <param name="pos"></param>
    public void SetDestination(Vector3 target)
    {
        m_navMeshAgent.SetDestination(target);
        m_animator.SetBool("Run", true);
    }
    /// <summary>
    /// 删除模型
    /// </summary>
    public virtual void DeleteObj()
    {
       
    }
}
