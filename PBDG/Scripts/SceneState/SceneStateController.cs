using UnityEngine;

/// <summary>
/// 场景状态控制者
/// </summary>
public class SceneStateController:FSMSystem
{
    public override void LoadScene(string stateName)
    {
        
        if (states.ContainsKey(stateName) == false)
        {
            Debug.LogError("在状态机里面不存在状态" + stateName + "，无法进行状态转换！"); return;
        }
        FSMState state = states["LoadScene"];
        currentState.DoAfterLeaving();
        currentState = state;
        currentStateName = "LoadScene";
        (state as LoadState).loadSceneName = stateName;
        currentState.DoBeforeEntering();
    }
}
