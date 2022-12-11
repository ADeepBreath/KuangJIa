
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class MoveUI : IUserInterface,IObserver
{
    //UI组件
    private Slider m_slider;
    private Button m_moveBtn;

    float timer = 0;
    int energyValue = 0;//当前能量值
    int maxValue;//最大能量值
    int count;//步数

    public int Count { get { return count; } }

    public MoveUI(Game game) : base(game)
    {
        Initialize();
    }

    public override void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("MovePanel");

        //能量条
        m_slider = UITool.GetUIComponent<Slider>(m_RootUI, "Slider");
        //抽卡按钮
        m_moveBtn = UITool.GetUIComponent<Button>(m_RootUI, "MoveBtn");
        m_moveBtn.onClick.AddListener(OnMoveBtnClick);

        MessageCenter.Instance.RegisterObservers(MessageID.ShowMovePanel, this);

        Show();
    }
    public override void Release()
    {
        m_RootUI = null;

        m_slider = null;
        m_moveBtn.onClick.RemoveListener(OnMoveBtnClick);
        m_moveBtn = null;

    }
    public void HandleNotification(MessageID id)
    {
        switch(id)
        {
            case MessageID.ShowMovePanel:
                {
                    Show();
                }
                break;
        }
    }
    public override void Update()
    {
        if(timer>0)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                Hide();
            }
        }
    }


    public override void Show()
    {
        m_RootUI.SetActive(true);
        timer = 3f;
        maxValue = Random.Range(16, 24);
    }
    public override void Hide()
    {
        m_RootUI.SetActive(false);
        count += (int)(m_slider.value * 4);

        if (count>0)
        {
            if(count>3)
            {
                count = 3;
            }
            MessageCenter.Instance.NotifyObserver(MessageID.PlayerMove);
            m_Game.SetHintContent("你行动" + count + "步！");
        }
        else
        {
            MessageCenter.Instance.NotifyObserver(MessageID.TurnOrgan);
            m_Game.SetHintContent("你触发了机关！");
        }

        count = 0;
        m_slider.value = 0;
        energyValue = 0;
    }

    /// <summary>
    /// 点击抽卡
    /// </summary>
    void OnMoveBtnClick()
    {
        energyValue++;
        m_slider.value = (float)energyValue / maxValue;
    }

}
