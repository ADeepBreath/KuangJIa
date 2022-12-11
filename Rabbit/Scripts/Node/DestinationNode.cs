
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 终点
/// </summary>
public class DestinationNode : INode
{
    public DestinationNode(GameObject obj) : base(obj)
    {
        this.type = NodeType.destination;
    }

}