/// <summary>
/// 开始状态
/// </summary>
public class StartState : FSMState
{
    public StartState(FSMSystem fsm) : base(fsm)
    {
        this.stateName = "StartScene";
    }

    public override void Act()
    {
        
    }

    public override void Reason()
    {
        fsm.LoadScene("MainMenuScene");
    }
}
