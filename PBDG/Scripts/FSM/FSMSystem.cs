using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 状态系统
/// </summary>
public class FSMSystem
{
    protected Dictionary<string, FSMState> states = new Dictionary<string, FSMState>();

    protected string currentStateName;
    protected FSMState currentState;

    public void Update()
    {
        currentState.Act();
        currentState.Reason();
    }
    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="state"></param>
    public void AddState(FSMState state)
    {
        if (state == null)
        {
            Debug.LogError("FSMState不能为空"); return;
        }
        if (states.ContainsKey(state.StateName))
        {
            Debug.LogError("状态" + state.StateName + "已经存在，无法重复添加"); return;
        }
        if (currentState == null)
        {
            currentState = state;
            currentStateName = state.StateName;
        }
        states.Add(state.StateName, state);
    }
    /// <summary>
    /// 删除状态
    /// </summary>
    /// <param name="stateName"></param>
    public void DeleteState(string stateName)
    {
        if (states.ContainsKey(stateName) == false)
        {
            Debug.LogError("无法删除不存在的状态：" + stateName); return;
        }
        states.Remove(stateName);
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="stateName"></param>
    public void PerformTransition(string stateName)
    {
        if (states.ContainsKey(stateName) == false)
        {
            Debug.LogError("在状态机里面不存在状态" + stateName + "，无法进行状态转换！"); return;
        }
        FSMState state = states[stateName];
        currentState.DoAfterLeaving();
        currentState = state;
        currentStateName = stateName;
        currentState.DoBeforeEntering();
    }
    public virtual void LoadScene(string stateName)
    {

    }
}

