using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据类型
/// </summary>
public enum ObjectInfoType
{
    MyPlayer,
    Enemy,
    Npc
}
/// <summary>
/// 对象基类数据
/// </summary>
public class ObjectInfoBase
{
    public string name;
    public string modelPath;
    public float[] position=new float[3];
    public float[] angle = new float[3];

}
