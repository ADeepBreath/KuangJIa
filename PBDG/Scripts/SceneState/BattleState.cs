
/// <summary>
/// 战斗状态
/// </summary>
public class BattleState : FSMState
{
    public BattleState(FSMSystem fsm) : base(fsm)
    {
        this.fsm = fsm;
        stateName = "BattleScene";
    }

    public override void DoBeforeEntering()
    {
        
    }
    public override void Act()
    {
        
    }

    public override void Reason()
    {
        
    }
    public override void DoAfterLeaving()
    {
        
    }
}
