using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public static GamePanel instance;

    private void Awake()
    {
        instance = this;
    }

    //数据
    MyPlayerInfo myPlayerInfo;
    Sprite[] heroSkillIcons = new Sprite[4];
    int initialHp, initialMp;

    //界面UI
    List<Button> skillBtns = new List<Button>();
    Image icon;
    Slider hpSlider, mpSlider;
    Text hpText,mpText;

    float recoverTimer;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerInfo = ObjectManager.GetInstance().myPlayerObj.m_info as MyPlayerInfo;
        ReadSkillIconsJson();
        GetUI();
        InitUi();
    }

    // Update is called once per frame
    void Update()
    {
        //恢复
        if (myPlayerInfo.hp != initialHp || myPlayerInfo.mp != initialMp)
        {
            recoverTimer += Time.deltaTime;
            if (recoverTimer > 5f)
            {
                recoverTimer -= 5f;
                //回血
                ObjectManager.GetInstance().myPlayerObj.ChangeHp(5);
                if(myPlayerInfo.hp>initialHp)
                {
                    myPlayerInfo.hp = initialHp;
                }
                //回蓝
                ObjectManager.GetInstance().myPlayerObj.ChangeMp(10);
                if (myPlayerInfo.mp > initialMp)
                {
                    myPlayerInfo.mp = initialMp;
                }
            }
        }

        //摇杆移动
        if (ObjectManager.GetInstance().myPlayerObj.m_go)
        {
            ObjectManager.GetInstance().myPlayerObj.MoveByJoystick();
        }
    }
    /// <summary>
    /// 初始化UI
    /// </summary>
    public void InitUi()
    {
        List<string> skillNames = ObjectManager.GetInstance().myPlayerObj.skillNames;
        for (int i = 0; i < skillNames.Count; i++)
        {
            skillBtns[i].GetComponent<Image>().sprite = heroSkillIcons[i];

            int j = i;
            skillBtns[i].onClick.AddListener(() =>
            {
                //足够蓝量
                if (myPlayerInfo.mp - (j + 1) * 5>=0)
                {
                    ObjectManager.GetInstance().myPlayerObj.PlaySkill(skillNames[j]);
                    ObjectManager.GetInstance().myPlayerObj.ChangeMp(-(j + 1) * 5);//扣蓝
                }
            });
        }
        icon.sprite = Resources.Load<Sprite>("HeroIcons/" + myPlayerInfo.name + "_Icon");
        initialHp = myPlayerInfo.hp;
        initialMp = myPlayerInfo.mp;
        RefreshHpAndMpView();
    }
    /// <summary>
    /// 刷新血量和魔法的显示
    /// </summary>
    public void RefreshHpAndMpView()
    {
        hpSlider.value = (float)myPlayerInfo.hp / initialHp;
        mpSlider.value = (float)myPlayerInfo.mp / initialMp;
        hpText.text= myPlayerInfo.hp+"/" + initialHp;
        mpText.text= myPlayerInfo.mp+"/" + initialMp;
    }
    /// <summary>
    /// 获取UI
    /// </summary>
    public void GetUI()
    {
        Transform skillBtnsGO = transform.Find("SkillBtns");
        for (int i = 0; i < skillBtnsGO.childCount; i++)
        {
            skillBtns.Add(skillBtnsGO.GetChild(i).GetComponent<Button>());
        }
        icon = transform.Find("Icon").GetComponent<Image>();
        hpSlider= transform.Find("HpSlider").GetComponent<Slider>();
        mpSlider = transform.Find("MpSlider").GetComponent<Slider>();
        hpText = hpSlider.transform.Find("HpText").GetComponent<Text>();
        mpText = mpSlider.transform.Find("MpText").GetComponent<Text>();
    }
    /// <summary>
    /// 读取技能图标
    /// </summary>
    void ReadSkillIconsJson()
    {
        if (File.Exists(myPlayerInfo.name + "SkillIcons.json"))
        {
            string[] heroSkillIconInfos;

            string s = File.ReadAllText(myPlayerInfo.name + "SkillIcons.json");
            heroSkillIconInfos = JsonConvert.DeserializeObject<string[]>(s);

            for (int i = 0; i < heroSkillIconInfos.Length; i++)
            {
                heroSkillIcons[i] = AssetDatabase.LoadAssetAtPath<Sprite>(heroSkillIconInfos[i]);
            }
        }
    }
}
