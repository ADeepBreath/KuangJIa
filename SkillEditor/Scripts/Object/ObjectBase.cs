using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ObjectBase
{
    public GameObject m_go;//����ģ��
    public ObjectInfoBase m_info;//��������

    //���
    public Animator m_animator;
    public NavMeshAgent m_navMeshAgent;


    public ObjectBase(ObjectInfoBase info)
    {
        m_info = info;
        CreateObj();
    }

    /// <summary>
    /// ����ģ��
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
    /// ������ģ��ʱ��ʼ�߼�
    /// </summary>
    public virtual void OnCreate()
    {
        m_animator = m_go.GetComponent<Animator>();
        m_navMeshAgent= m_go.AddComponent<NavMeshAgent>();
        m_go.AddComponent<LocalNavMeshBuilder>();
    }
    /// <summary>
    /// ����λ��
    /// </summary>
    /// <param name="pos"></param>
    public void SetPos(Vector3 pos)
    {
        m_go.transform.position = pos;
    }
    /// <summary>
    /// ������Ŀ�ĵ�
    /// </summary>
    /// <param name="pos"></param>
    public void SetDestination(Vector3 target)
    {
        m_navMeshAgent.SetDestination(target);
        m_animator.SetBool("Run", true);
    }
    /// <summary>
    /// ɾ��ģ��
    /// </summary>
    public virtual void DeleteObj()
    {
       
    }
}
