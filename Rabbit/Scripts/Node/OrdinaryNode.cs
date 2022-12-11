using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ͨ�ڵ�
/// </summary>
public class OrdinaryNode : INode
{
    public OrdinaryNode(GameObject obj) : base(obj)
    {
        this.type = NodeType.ordinary;
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void ToRandomTrap()
    {
        obj.GetComponent<MeshRenderer>().material.color = Color.black;
    }
    /// <summary>
    /// �����ͨ�ڵ�
    /// </summary>
    public void ToOrdinaryNode()
    {
        obj.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
