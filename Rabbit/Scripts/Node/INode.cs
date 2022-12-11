
using UnityEngine;

/// <summary>
///节点类型
/// </summary>
public enum NodeType
{
    empty,//空
    ordinary,//普通
    tarp,//陷阱
    destination,//终点
}
/// <summary>
/// 节点
/// </summary>
public class INode
{
    protected NodeType type;
    protected GameObject obj;

    public INode(GameObject obj)
    {
        this.type = NodeType.empty;
        this.obj = obj;
    }
    /// <summary>
    /// 获取节点位置
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPos()
    {
        if(obj == null)
        {
            Debug.LogError("获取一个不存在的节点位置。");
            return Vector3.zero;
        }
        return obj.transform.position;

    }
}
