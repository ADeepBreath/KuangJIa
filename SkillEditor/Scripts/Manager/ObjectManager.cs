using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public MyPlayer myPlayerObj;
    public List<EnemyObject> enemys = new List<EnemyObject>();
    public List<NpcObject> npcs = new List<NpcObject>();


    public void Start()
    {
      
    }

    public void Update()
    {
        
    }
    /// <summary>
    /// ��ʼ����������
    /// </summary>
    public void InitObject()
    {
        ReadMyPlayerInfo();
        ReadEnemyInfo();
        ReadNpcInfo();
    }
    /// <summary>
    /// ��ȡ�ҵ������Ϣ������
    /// </summary>
    public void ReadMyPlayerInfo()
    {
        if(File.Exists(ObjectInfoType.MyPlayer.ToString()+".json"))
        {
            string s = File.ReadAllText(ObjectInfoType.MyPlayer.ToString() + ".json");
            Dictionary<string,MyPlayerInfo> myPlayerInfo = JsonConvert.DeserializeObject<Dictionary<string,MyPlayerInfo>>(s);
            foreach (var myPlayer in myPlayerInfo.Values)
            {
                myPlayerObj = new MyPlayer(myPlayer);

                break;
            }
        }
    }
    /// <summary>
    /// ��ȡ������Ϣ������
    /// </summary>
    public void ReadEnemyInfo()
    {
        if (File.Exists(ObjectInfoType.Enemy.ToString() + ".json"))
        {
            string s = File.ReadAllText(ObjectInfoType.Enemy.ToString() + ".json");
            Dictionary<string, EnemyInfo> enemyInfos = JsonConvert.DeserializeObject<Dictionary<string, EnemyInfo>>(s);
            foreach (var enemyInfo in enemyInfos.Values)
            {
                enemys.Add(new EnemyObject(enemyInfo));
            }
        }
    }
    /// <summary>
    /// ��ȡNpc��Ϣ������
    /// </summary>
    public void ReadNpcInfo()
    {
        if (File.Exists(ObjectInfoType.Npc.ToString() + ".json"))
        {
            string s = File.ReadAllText(ObjectInfoType.Npc.ToString() + ".json");
            Dictionary<string, NpcInfo> npcInfos = JsonConvert.DeserializeObject<Dictionary<string, NpcInfo>>(s);
            foreach (var npcInfo in npcInfos.Values)
            {
                npcs.Add(new NpcObject(npcInfo));
            }
        }
    }
}
