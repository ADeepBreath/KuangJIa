
using Codice.Client.BaseCommands;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



public class SceneEditor : EditorWindow
{
    //数据
    Dictionary<ObjectInfoType, Dictionary<string, ObjectInfoBase>> objectInfos = new Dictionary<ObjectInfoType, Dictionary<string, ObjectInfoBase>>();
    List<string> names = new List<string>();
    string[] modelTypes = { "Player", "Enemy", "Npc" };
    ObjectInfoBase objectInfo=new MyPlayerInfo();

    //编辑器变量
    int id = -1;
    ObjectInfoType objectInfoType;
    GameObject go;

    [MenuItem("Tools/SceneEditor")]
    public static void Init()
    {
        //编辑模式，可打开场景编辑界面
        if (GameManager.GetInstance().pattern==1)
        {
            SceneEditor window = GetWindow<SceneEditor>("场景编辑器");
            window.minSize = new Vector2(410, 400);
            window.maxSize = new Vector2(410, 400);
            window.Show();
        }
    }
    private void OnEnable()
    {
        ReadHeroFilesToList();
        ReadTypeJson();
    }
    private void OnDisable()
    {
        id = -1;
        if (go)
        {
            DestroyImmediate(go);
        }
    }
    private void OnGUI()
    {
        //选择类型
        GUILayout.BeginHorizontal();
        {
            ObjectInfoType objectTypeIndex = (ObjectInfoType)EditorGUILayout.EnumPopup(objectInfoType);
            if(objectTypeIndex!= objectInfoType)
            {
                objectInfoType = objectTypeIndex;
                ReadTypeJson();
                id = -1;
                if (go)
                {
                    DestroyImmediate(go);
                }
            }
        }
        GUILayout.EndHorizontal();

        //选择模型
        GUILayout.BeginHorizontal();
        {
            int modelId = EditorGUILayout.Popup(id, names.ToArray());
            if (id != modelId)
            {
                id = modelId;

                //删除上一个模型
                if (go)
                {
                    DestroyImmediate(go);
                }

                //读取原有配置信息
                switch ((int)objectInfoType)
                {
                    case 0: objectInfo = new MyPlayerInfo(); break;
                    case 1: objectInfo = new EnemyInfo(); break;
                    case 2: objectInfo = new NpcInfo(); break;
                }
                if (objectInfos.ContainsKey(objectInfoType))
                {
                    if (objectInfos[objectInfoType].ContainsKey(names[id]))
                    {
                        objectInfo = objectInfos[objectInfoType][names[id]];
                    }
                }

                //实例模型
                go = Instantiate(Resources.Load<GameObject>("Heros/" + names[id]));
                go.name = names[id];
                objectInfo.name = names[id];
                objectInfo.modelPath = "Heros/" + names[id];

                //初始位置和角度
                go.transform.position = new Vector3(objectInfo.position[0], objectInfo.position[1], objectInfo.position[2]);
                go.transform.eulerAngles = new Vector3(objectInfo.angle[0], objectInfo.angle[1], objectInfo.angle[2]);

                //设置场景编辑虚拟相机
                VirtualCameraManager.GetInstance().SelectVC("SceneEditorVC", go.transform);
            }
        }
        GUILayout.EndHorizontal();

        if(go)
        {
            GUILayout.Label("设置位置和角度，可从场景摆放：");
            //设置位置
            GUILayout.BeginHorizontal();
            {
                objectInfo.position[0] = go.transform.position.x;
                objectInfo.position[1] = go.transform.position.y;
                objectInfo.position[2] = go.transform.position.z;

                GUILayout.Label("位置：", GUILayout.Width(60));
                GUILayout.Label("X：", GUILayout.Width(30));
                objectInfo.position[0] = EditorGUILayout.FloatField(objectInfo.position[0], GUILayout.Width(60));
                GUILayout.Label("Y：", GUILayout.Width(30));
                objectInfo.position[1] = EditorGUILayout.FloatField(objectInfo.position[1], GUILayout.Width(60));
                GUILayout.Label("Z：", GUILayout.Width(30));
                objectInfo.position[2] = EditorGUILayout.FloatField(objectInfo.position[2], GUILayout.Width(60));

                go.transform.position = new Vector3(objectInfo.position[0], objectInfo.position[1], objectInfo.position[2]);
            }
            GUILayout.EndHorizontal();

            //设置角度
            GUILayout.BeginHorizontal();
            {
                objectInfo.angle[0] = go.transform.eulerAngles.x;
                objectInfo.angle[1] = go.transform.eulerAngles.y;
                objectInfo.angle[2] = go.transform.eulerAngles.z;

                GUILayout.Label("角度：", GUILayout.Width(60));
                GUILayout.Label("X：", GUILayout.Width(30));
                objectInfo.angle[0] = EditorGUILayout.FloatField(objectInfo.angle[0], GUILayout.Width(60));
                GUILayout.Label("Y：", GUILayout.Width(30));
                objectInfo.angle[1] = EditorGUILayout.FloatField(objectInfo.angle[1], GUILayout.Width(60));
                GUILayout.Label("Z：", GUILayout.Width(30));
                objectInfo.angle[2] = EditorGUILayout.FloatField(objectInfo.angle[2], GUILayout.Width(60));

                go.transform.eulerAngles = new Vector3(objectInfo.angle[0], objectInfo.angle[1], objectInfo.angle[2]);
            }
            GUILayout.EndHorizontal();

            switch ((int)objectInfoType)
            {
                case 0:
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("血量：", GUILayout.Width(60));
                            (objectInfo as MyPlayerInfo).hp = EditorGUILayout.IntSlider((objectInfo as MyPlayerInfo).hp, 50, 150, GUILayout.Width(250));
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("蓝量：", GUILayout.Width(60));
                            (objectInfo as MyPlayerInfo).mp = EditorGUILayout.IntSlider((objectInfo as MyPlayerInfo).mp, 50, 150, GUILayout.Width(250));
                        }
                        GUILayout.EndHorizontal();
                    }
                    break;
                case 1:
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("血量：", GUILayout.Width(60));
                            (objectInfo as EnemyInfo).hp = EditorGUILayout.IntSlider((objectInfo as EnemyInfo).hp, 50, 100, GUILayout.Width(250));
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("警戒距离：", GUILayout.Width(60));
                            (objectInfo as EnemyInfo).distance = EditorGUILayout.IntSlider((objectInfo as EnemyInfo).distance, 8, 15, GUILayout.Width(250));
                        }
                        GUILayout.EndHorizontal();
                    }
                    break;
                case 2:
                    {
                        GUILayout.BeginHorizontal();
                        {
                            GUILayout.Label("触发距离：", GUILayout.Width(60));
                            (objectInfo as NpcInfo).triggerDistance = EditorGUILayout.IntSlider((objectInfo as NpcInfo).triggerDistance, 2, 5, GUILayout.Width(250));
                        }
                        GUILayout.EndHorizontal();
                    }
                    break;
            }

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(70);
                if (GUILayout.Button("保存", GUILayout.Width(100), GUILayout.Height(30)))
                {
                    AddRoleInfo();
                    SaveInfoToJson();
                }
                GUILayout.Space(70);
                if (GUILayout.Button("删除", GUILayout.Width(100), GUILayout.Height(30)))
                {
                    DeleteRoleInfo();
                    SaveInfoToJson();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
        }     
    }
    /// <summary>
    /// 删除此角色在场景中的信息
    /// </summary>
    void DeleteRoleInfo()
    {
        if (objectInfos.ContainsKey(objectInfoType))
        {
            if (objectInfos[objectInfoType].ContainsKey(names[id]))
            {
                objectInfos[objectInfoType].Remove(names[id]);
            }
        }
    }
    /// <summary>
    /// 读取对应类型信息
    /// </summary>
    void ReadTypeJson()
    {
        //清空对应类型原有的信息
        if (!objectInfos.ContainsKey(objectInfoType))
        {
            objectInfos.Add(objectInfoType, new Dictionary<string, ObjectInfoBase>());
            if (objectInfos[objectInfoType].Count>0)
            {
                objectInfos[objectInfoType].Clear();
            }
        }
        //读取表中对应类型的信息
        if (File.Exists(objectInfoType.ToString() + ".json"))
        {
            string s = File.ReadAllText(objectInfoType.ToString() + ".json");
            if (objectInfoType == ObjectInfoType.MyPlayer)
            {
                Dictionary<string, MyPlayerInfo> myPlayerInfo = JsonConvert.DeserializeObject<Dictionary<string, MyPlayerInfo>>(s);
                foreach (var myPlayer in myPlayerInfo)
                {
                    objectInfos[objectInfoType].Add(myPlayer.Key, myPlayer.Value);
                }    
            }
            if (objectInfoType == ObjectInfoType.Enemy)
            {
                Dictionary<string, EnemyInfo> enemyInfos = JsonConvert.DeserializeObject<Dictionary<string, EnemyInfo>>(s);
                foreach (var enemy in enemyInfos)
                {
                    objectInfos[objectInfoType].Add(enemy.Key, enemy.Value);
                }
            }
            if (objectInfoType == ObjectInfoType.Npc)
            {
                Dictionary<string, NpcInfo> npcInfos = JsonConvert.DeserializeObject<Dictionary<string, NpcInfo>>(s);
                foreach (var npc in npcInfos)
                {
                    objectInfos[objectInfoType].Add(npc.Key, npc.Value);
                }
            }
        }
    }
    /// <summary>
    /// 添加此角色信息
    /// </summary>
    void AddRoleInfo()
    {
        //提前创建此类型
        if(!objectInfos.ContainsKey(objectInfoType))
        {
            objectInfos.Add(objectInfoType, new Dictionary<string,ObjectInfoBase>()); 
        }

        //我的玩家唯一
        if (objectInfoType==ObjectInfoType.MyPlayer)
        {
            if (objectInfos[objectInfoType].Count>0)
            {
                objectInfos[objectInfoType].Clear();
            }
        }

        //添加此角色信息
        if (!objectInfos[objectInfoType].ContainsKey(names[id]))
        {
            objectInfos[objectInfoType].Add(names[id], objectInfo);
        }
        else
        {
            objectInfos[objectInfoType][names[id]] = objectInfo;
        }
    }
    /// <summary>
    /// 保存成json配置表
    /// </summary>
    public void SaveInfoToJson()
    {
        if (objectInfoType == ObjectInfoType.MyPlayer)
        {
            Dictionary<string, MyPlayerInfo> myPlayerInfo = new Dictionary<string, MyPlayerInfo>();
            foreach (var objectInfo in objectInfos[objectInfoType])
            {
                myPlayerInfo.Add(objectInfo.Key, objectInfo.Value as MyPlayerInfo);
            }
            string s = JsonConvert.SerializeObject(myPlayerInfo);
            File.WriteAllText(objectInfoType.ToString() + ".json",s);
        }
        else if (objectInfoType == ObjectInfoType.Enemy)
        {
            Dictionary<string, EnemyInfo> enemyInfos = new Dictionary<string, EnemyInfo>();
            foreach (var objectInfo in objectInfos[objectInfoType])
            {
                enemyInfos.Add(objectInfo.Key, objectInfo.Value as EnemyInfo);
            }
            string s = JsonConvert.SerializeObject(enemyInfos);
            File.WriteAllText(objectInfoType.ToString() + ".json", s);
        }
        else if (objectInfoType == ObjectInfoType.Npc)
        {
            Dictionary<string, NpcInfo> npcInfos = new Dictionary<string, NpcInfo>();
            foreach (var objectInfo in objectInfos[objectInfoType])
            {
                npcInfos.Add(objectInfo.Key, objectInfo.Value as NpcInfo);
            }
            string s = JsonConvert.SerializeObject(npcInfos);
            File.WriteAllText(objectInfoType.ToString() + ".json", s);
        }

    }
    /// <summary>
    /// 读取英雄列表信息
    /// </summary>
    void ReadHeroFilesToList()
    {
        names.Clear();

        string[] paths = Directory.GetFiles("Assets/Resources/Heros");
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].EndsWith(".prefab"))
            {
                string s = paths[i].Replace("Assets/Resources/Heros\\", "");
                s = s.Replace(".prefab", "");
                names.Add(s);
            }
        }
    }
}
