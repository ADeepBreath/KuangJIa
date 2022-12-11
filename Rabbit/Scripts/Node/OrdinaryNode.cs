using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通节点
/// </summary>
public class OrdinaryNode : INode
{
    public OrdinaryNode(GameObject obj) : base(obj)
    {
        this.type = NodeType.ordinary;
    }

    /// <summary>
    /// 变成随机陷阱
    /// </summary>
    public void ToRandomTrap()
    {
        obj.GetComponent<MeshRenderer>().material.color = Color.black;
    }
    /// <summary>
    /// 变成普通节点
    /// </summary>
    public void ToOrdinaryNode()
    {
        obj.GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
