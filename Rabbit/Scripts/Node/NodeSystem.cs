
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

/// <summary>
/// 节点系统
/// </summary>
public class NodeSystem : IGameSystem,IObserver
{
    List<INode> nodes = new List<INode>();
    GameObject organNode;
    int currentRandomTarpIndex = -2;//当前随机陷阱下标
    List<int> tarpIndexs = new List<int>();
    bool isOrgan;//机关是否运行

    public int NodeCount { get => nodes.Count; }
    public NodeSystem(Game game) : base(game)
    {
        Initialize();
    }

    public override void Initialize()
    {
        FindAllNodes();

        MessageCenter.Instance.RegisterObservers(MessageID.TurnOrgan, this);
    }
    public override void Release()
    {
        nodes.Clear();
        organNode = null;
        tarpIndexs.Clear();

        MessageCenter.Instance.RemoveObservers(MessageID.TurnOrgan, this);
    }
    public void HandleNotification(MessageID id)
    {
        if (id == MessageID.TurnOrgan)
        {
            TurnOrgan();
        }

    }
    /// <summary>
    /// 查找所有节点
    /// </summary>
    public void FindAllNodes()
    {
        //路径节点
        GameObject nodesObj = UnityTool.FindGameObject("Nodes");
        for (int i = 0; i < nodesObj.transform.childCount; i++)
        {
            GameObject node = nodesObj.transform.GetChild(i).gameObject;
            if (node.name == "OrdinaryNode")
            {
                nodes.Add(new OrdinaryNode(node));
                node.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else if (node.name == "DestinationNode")
            {
                nodes.Add(new DestinationNode(node));
                node.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            else
            {
                nodes.Add(new TarpNode(node));
                tarpIndexs.Add(i);
                node.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
        }
        //机关节点
        organNode = UnityTool.FindGameObject("OrganNode");
        organNode.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }
    float timer = 0;
    public override void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;
            organNode.transform.Rotate(Vector3.up * Time.deltaTime * 100f);

            if (timer<=1f&&timer>0.9f)
            {
                timer = 0.9f;
                CreateRandomTrap();
                TriggetTarp();
            }
            if(timer<=0f)
            {
                RecoverTarp();
                MessageCenter.Instance.NotifyObserver(MessageID.SwitchoverCurrentPlayer);
            }
        }
    }
    /// <summary>
    /// 转动机关
    /// </summary>
    public void TurnOrgan()
    {
        timer = 3f; 
    }
    /// <summary>
    /// 触发陷阱
    /// </summary>
    public void TriggetTarp()
    {
        isOrgan = true;
        for (int i = 0; i < tarpIndexs.Count-1; i++)
        {
            (nodes[tarpIndexs[i]] as TarpNode).Trigger();
        }
        MessageCenter.Instance.NotifyObserver(MessageID.IsTriggerTarp);
    }
    public void RecoverTarp()
    {
        isOrgan=false;
        for (int i = 0; i < tarpIndexs.Count - 1; i++)
        {
            (nodes[tarpIndexs[i]] as TarpNode).Recover();
        }
    }
    public void CreateRandomTrap()
    {
        if (currentRandomTarpIndex != -2)
        {
            (nodes[currentRandomTarpIndex] as OrdinaryNode).ToOrdinaryNode();
            tarpIndexs.Remove(currentRandomTarpIndex);
        }
        int RandomTrapIndex = 0;
        //生成随机陷阱
        while (true)
        {
            RandomTrapIndex = Random.Range(0, nodes.Count);
            if (nodes[RandomTrapIndex] is OrdinaryNode)
            {
                (nodes[RandomTrapIndex] as OrdinaryNode).ToRandomTrap();
                break;
            }
        }
        currentRandomTarpIndex = RandomTrapIndex;
        tarpIndexs.Add(currentRandomTarpIndex);
    }
    /// <summary>
    /// 获取一段路径位置
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public List<Vector3> GetRoutePos(int start,int end)
    {
        List<Vector3> pos=new List<Vector3>();
        if(end>nodes.Count-1)
        {
            end= nodes.Count-1;
        }
        for (int i = start+1; i <= end; i++)
        {
            pos.Add(nodes[i].GetPos());
        }
        return pos;
    }
    /// <summary>
    /// 获取陷阱
    /// </summary>
    /// <returns></returns>
    public List<int> GetTarpIndex()
    {
        if(isOrgan)
        {
            return tarpIndexs;
        }
        List<int> list = new List<int>();
        list.Add(tarpIndexs[tarpIndexs.Count-1]);
        return list;
    }
 
}
