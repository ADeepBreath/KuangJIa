using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 主菜单状态
/// </summary>
public class MainMenuState : FSMState
{
    bool isPlay;
    public MainMenuState(FSMSystem fsm) : base(fsm)
    {
        this.fsm = fsm;
        stateName = "MainMenuScene";
    }

    public override void DoBeforeEntering()
    {
        //取得开始游戏按钮
        GameObject.Find("PlayGameBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            isPlay=true;
        });
    }
    public override void Act()
    {
        
    }

    public override void Reason()
    {
        if(isPlay)
        {
            fsm.LoadScene("BattleScene");
        }
    }
}
