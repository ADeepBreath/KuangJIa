using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景状态控制者
/// </summary>
public class SceneStateCollecter
{
    Dictionary<string, ISceneState> sceneMap = new Dictionary<string, ISceneState>();

    string currentId = null;
    ISceneState currentState = null;

    AsyncOperation async;

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <param name="id"></param>
    /// <param name="state"></param>
    public void AddState(ISceneState state)
    {
        if(state==null)
        {
            Debug.LogError("要添加的状态"+state.ID+"本身不能为空！");
            return;
        }
        if(sceneMap.ContainsKey(state.ID))
        {
            Debug.LogError("场景状态" + state.ID + "已存在，无法重复添加！");
            return;
        }
        if(currentId==null)
        {
            currentId = state.ID;
            currentState = state;
        }
        sceneMap.Add(state.ID, state);
    }
    /// <summary>
    /// 移除状态
    /// </summary>
    /// <param name="id"></param>
    public void RemoveState(string id)
    {
        if (id == null)
        {
            Debug.LogError("要移除的状态ID不能为空！");
            return;
        }
        if(!sceneMap.ContainsKey(id))
        {
            Debug.Log("要移除的状态ID不存在！");
        }
        sceneMap.Remove(id);
    }
    public void Update()
    {
        if (currentId == null || currentState == null)
        {
            return;
        }
        currentState.Update();
        currentState.Reason();
    }
    /// <summary>
    /// 转换状态
    /// </summary>
    /// <param name="id"></param>
    public void TransitionState(string id)
    {
        if(id == null)
        {
            Debug.LogError("要转换的状态"+id+"不能为空！");
            return;
        }
        if(!sceneMap.ContainsKey(id))
        {
            Debug.LogError("要转换的状态"+id+"不存在！");
            return;
        }
        if(id==currentId)
        {
            return;
        }
        ISceneState state=sceneMap[id];
        currentState.After();
        currentId = id;
        async = SceneManager.LoadSceneAsync(id);
        async.completed += ((async) =>
        {
            currentState = state;
            currentState.Before();
        });
    }
}
