using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillEditor : EditorWindow
{
    //数据
    static List<string> heroNames = new List<string>();
    static Dictionary<string, SkillInfos> skillInfos = new Dictionary<string, SkillInfos>();

    //编辑器变量
    static int id = -1;
    string skillName;
    Texture2D heroIcon;
    Texture2D[] heroSkillIcons = new Texture2D[4];

    public static GameObject go;

    [MenuItem("Tools/SkillEditor")]
    public static void Init()
    {
        //编辑模式，可打开创建英雄界面
        if (GameManager.GetInstance().pattern==1)
        {
            SkillEditor window = GetWindow<SkillEditor>("技能编辑器");
            window.Show();
        }
    }

    private void OnEnable()
    {
        ReadHeroFilesToList();
        ReadSkillInfos();
    }
    private void OnDisable()
    {
        SaveSkillIconsJson();
        id = -1;
        if (go)
        {
            DestroyImmediate(go);
        }
    }
    private void OnGUI()
    {
        //选择英雄模型
        GUILayout.BeginHorizontal();
        {
            int heroId = EditorGUILayout.Popup(id, heroNames.ToArray());
            if (id != heroId)
            {
                SaveSkillIconsJson();

                id = heroId;

                //删除上一个模型
                if (go)
                {
                    DestroyImmediate(go);
                }

                //头像
                heroIcon = Resources.Load<Texture2D>("HeroIcons/" + heroNames[id] + "_Icon");
                ReadSkillIconsJson();

                //实例模型
                go = Instantiate(Resources.Load<GameObject>("Heros/" + heroNames[id]));
                go.name = heroNames[id];

                ReadSkillInfos();

                //添加播放技能逻辑
                go.AddComponent<AudioSource>();
                go.AddComponent<PlaySkillComponent>();

                //设置技能编辑虚拟相机
                VirtualCameraManager.GetInstance().SelectVC("SkillEditorVC", go.transform);

            }
        }
        GUILayout.EndHorizontal();

        if (go)
        {
            //头像
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("头像：", GUILayout.Width(40));
                GUILayout.Box(heroIcon, GUILayout.Width(100), GUILayout.Height(100));
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("技能图标：");
            GUILayout.BeginHorizontal();
            {
                for (int i = 0; i < heroSkillIcons.Length; i++)
                {
                    GUILayout.BeginVertical();
                    {
                        GUILayout.Box(heroSkillIcons[i], GUILayout.Width(70), GUILayout.Height(70));
                        heroSkillIcons[i] = (Texture2D)EditorGUILayout.ObjectField(heroSkillIcons[i], typeof(Texture2D), false, GUILayout.Width(80));
                    }
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndHorizontal();

            //创建技能
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("输入技能名称：", GUILayout.Width(90));
                skillName = GUILayout.TextField(skillName);
                if (GUILayout.Button("Add"))
                {
                    if (skillInfos.Count < 4)
                    {
                        if (!skillInfos.ContainsKey(skillName))
                        {
                            skillInfos.Add(skillName, new SkillInfos());
                        }
                        else
                        {
                            Debug.Log("已存在同名技能！");
                        }
                    }
                    else
                    {
                        Debug.Log("最多只能创建四个技能！");
                    }

                }
            }
            GUILayout.EndHorizontal();


            //技能列表
            foreach (var item in skillInfos)
            {
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button(item.Key))
                    {
                        SkillComponentEditor.Init(item.Key, item.Value);
                    }
                }
                GUILayout.EndHorizontal();
            }
        }

    }
    /// <summary>
    /// 读取技能信息
    /// </summary>
    public void ReadSkillInfos()
    {
        if(id!=-1)
        {
            if (File.Exists(heroNames[id] + "Skills.json"))
            {
                string s = File.ReadAllText(heroNames[id] + "Skills.json");
                skillInfos = JsonConvert.DeserializeObject<Dictionary<string, SkillInfos>>(s);
            }
            else
            {
                skillInfos=new Dictionary<string, SkillInfos>();
            }
        }
    }
    /// <summary>
    /// 保存技能信息
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="skillInfos"></param>
    public static void SaveSkillInfos(string skillName,SkillInfos Infos)
    {
        skillInfos[skillName] = Infos;

        string s = JsonConvert.SerializeObject(skillInfos);
        File.WriteAllText(heroNames[id] + "Skills.json", s);
    }
    /// <summary>
    /// 保存技能图标
    /// </summary>
    void SaveSkillIconsJson()
    {
        List<string> heroSkillIconInfos = new List<string>();

        for (int i = 0; i < heroSkillIcons.Length; i++)
        {
            if (heroSkillIcons[i]!=null)
            {
                heroSkillIconInfos.Add(AssetDatabase.GetAssetPath(heroSkillIcons[i]));
            }
        }

        //转换成Json文件
        if (heroSkillIconInfos.Count>0)
        {
            string s = JsonConvert.SerializeObject(heroSkillIconInfos);
            File.WriteAllText(heroNames[id] + "SkillIcons.json", s);
        }

    }
    /// <summary>
    /// 读取技能图标
    /// </summary>
    void ReadSkillIconsJson()
    {
        if (File.Exists(heroNames[id] + "SkillIcons.json"))
        {
            string[] heroSkillIconInfos;

            string s = File.ReadAllText(heroNames[id] + "SkillIcons.json");
            heroSkillIconInfos = JsonConvert.DeserializeObject<string[]>(s);

            for (int i = 0; i < heroSkillIconInfos.Length; i++)
            {
                heroSkillIcons[i] = AssetDatabase.LoadAssetAtPath<Texture2D>(heroSkillIconInfos[i]);
            }
        }
    }
    /// <summary>
    /// 读取英雄列表信息
    /// </summary>
    void ReadHeroFilesToList()
    {
        heroNames.Clear();

        string[] paths = Directory.GetFiles("Assets/Resources/Heros");
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i].EndsWith(".prefab"))
            {
                string s = paths[i].Replace("Assets/Resources/Heros\\", "");
                s = s.Replace(".prefab", "");
                heroNames.Add(s);
            }
        }
    }
}
