using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillEditor : EditorWindow
{
    //����
    static List<string> heroNames = new List<string>();
    static Dictionary<string, SkillInfos> skillInfos = new Dictionary<string, SkillInfos>();

    //�༭������
    static int id = -1;
    string skillName;
    Texture2D heroIcon;
    Texture2D[] heroSkillIcons = new Texture2D[4];

    public static GameObject go;

    [MenuItem("Tools/SkillEditor")]
    public static void Init()
    {
        //�༭ģʽ���ɴ򿪴���Ӣ�۽���
        if (GameManager.GetInstance().pattern==1)
        {
            SkillEditor window = GetWindow<SkillEditor>("���ܱ༭��");
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
        //ѡ��Ӣ��ģ��
        GUILayout.BeginHorizontal();
        {
            int heroId = EditorGUILayout.Popup(id, heroNames.ToArray());
            if (id != heroId)
            {
                SaveSkillIconsJson();

                id = heroId;

                //ɾ����һ��ģ��
                if (go)
                {
                    DestroyImmediate(go);
                }

                //ͷ��
                heroIcon = Resources.Load<Texture2D>("HeroIcons/" + heroNames[id] + "_Icon");
                ReadSkillIconsJson();

                //ʵ��ģ��
                go = Instantiate(Resources.Load<GameObject>("Heros/" + heroNames[id]));
                go.name = heroNames[id];

                ReadSkillInfos();

                //��Ӳ��ż����߼�
                go.AddComponent<AudioSource>();
                go.AddComponent<PlaySkillComponent>();

                //���ü��ܱ༭�������
                VirtualCameraManager.GetInstance().SelectVC("SkillEditorVC", go.transform);

            }
        }
        GUILayout.EndHorizontal();

        if (go)
        {
            //ͷ��
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("ͷ��", GUILayout.Width(40));
                GUILayout.Box(heroIcon, GUILayout.Width(100), GUILayout.Height(100));
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("����ͼ�꣺");
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

            //��������
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("���뼼�����ƣ�", GUILayout.Width(90));
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
                            Debug.Log("�Ѵ���ͬ�����ܣ�");
                        }
                    }
                    else
                    {
                        Debug.Log("���ֻ�ܴ����ĸ����ܣ�");
                    }

                }
            }
            GUILayout.EndHorizontal();


            //�����б�
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
    /// ��ȡ������Ϣ
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
    /// ���漼����Ϣ
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
    /// ���漼��ͼ��
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

        //ת����Json�ļ�
        if (heroSkillIconInfos.Count>0)
        {
            string s = JsonConvert.SerializeObject(heroSkillIconInfos);
            File.WriteAllText(heroNames[id] + "SkillIcons.json", s);
        }

    }
    /// <summary>
    /// ��ȡ����ͼ��
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
    /// ��ȡӢ���б���Ϣ
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
