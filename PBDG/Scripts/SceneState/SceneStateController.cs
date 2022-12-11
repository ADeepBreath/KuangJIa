using UnityEngine;

/// <summary>
/// ����״̬������
/// </summary>
public class SceneStateController:FSMSystem
{
    public override void LoadScene(string stateName)
    {
        
        if (states.ContainsKey(stateName) == false)
        {
            Debug.LogError("��״̬�����治����״̬" + stateName + "���޷�����״̬ת����"); return;
        }
        FSMState state = states["LoadScene"];
        currentState.DoAfterLeaving();
        currentState = state;
        currentStateName = "LoadScene";
        (state as LoadState).loadSceneName = stateName;
        currentState.DoBeforeEntering();
    }
}
