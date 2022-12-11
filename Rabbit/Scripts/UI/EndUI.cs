using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 结束面板
/// </summary>
public class EndUI : IUserInterface,IObserver
{
    private Text m_text;
    private Button m_returnBtn;

    public EndUI(Game game) : base(game)
    {
        Initialize();
    }
    public override void Initialize()
    {
        m_RootUI = UnityTool.FindGameObject("EndPanel");
        m_text = UITool.GetUIComponent<Text>(m_RootUI, "Text");
        m_returnBtn = UITool.GetUIComponent<Button>(m_RootUI, "ReturnBtn");
        m_returnBtn.onClick.AddListener(OnReturnBtnClick);

        Hide();

        MessageCenter.Instance.RegisterObservers(MessageID.Win,this);
        MessageCenter.Instance.RegisterObservers(MessageID.Lose, this);
    }

    public override void Release()
    {
        m_RootUI=null;
        m_text = null;
        m_returnBtn.onClick.RemoveListener(OnReturnBtnClick);
        m_returnBtn = null;

        MessageCenter.Instance.RemoveObservers(MessageID.Win, this);
        MessageCenter.Instance.RemoveObservers(MessageID.Lose, this);
    }
    public void HandleNotification(MessageID id)
    {
        switch (id)
        {
            case MessageID.Lose:
                {
                    End("你败了！");
                }
                break;
            case MessageID.Win:
                {
                    End("WIN！");
                }
                break;

        }
    }
    /// <summary>
    /// 结束
    /// </summary>
    /// <param name="content"></param>
    public void End(string content)
    {
        Time.timeScale = 0;
        m_text.text=content;
        Show();
    }
    /// <summary>
    /// 重新游戏
    /// </summary>
    public void OnReturnBtnClick()
    {
        MessageCenter.Instance.NotifyObserver(MessageID.Afresh);
    }

}
