using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÏÝÚå½Úµã
/// </summary>
public class TarpNode : INode
{
    public TarpNode(GameObject obj) : base(obj)
    {
        this.type = NodeType.tarp;
    }
    /// <summary>
    /// ´¥·¢
    /// </summary>
    public void Trigger()
    {
        obj.SetActive(false);
    }
    /// <summary>
    /// »Ö¸´
    /// </summary>
    public void Recover()
    {
        obj.SetActive(true);
    }
}
