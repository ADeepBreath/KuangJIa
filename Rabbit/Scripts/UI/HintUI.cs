using UnityEngine;
using UnityEngine.UI;

public class HintUI : IUserInterface
{
    private Text m_text;

    float timer;
    public HintUI(Game game) : base(game)
    {
        Initialize();
    }

    public override void Initialize()
    {
        m_RootUI = UnityTool.FindGameObject("HintPanel");
        m_text = UITool.GetUIComponent<Text>(m_RootUI, "Text");
        Hide();
    }
    public override void Release()
    {
        m_RootUI = null;
        m_text = null;
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
    }
    public override void Hide()
    {
        m_RootUI.SetActive(false);
    }
    /// <summary>
    /// 写入内容
    /// </summary>
    /// <param name="content"></param>
    public void SetContent(string content)
    {
        m_text.text = content;
        Show();
        timer = 2f;
    }
}
