using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����״̬������
/// </summary>
public class SceneStateCollecter
{
    Dictionary<string, ISceneState> sceneMap = new Dictionary<string, ISceneState>();

    string currentId = null;
    ISceneState currentState = null;

    AsyncOperation async;

    /// <summary>
    /// ���״̬
    /// </summary>
    /// <param name="id"></param>
    /// <param name="state"></param>
    public void AddState(ISceneState state)
    {
        if(state==null)
        {
            Debug.LogError("Ҫ��ӵ�״̬"+state.ID+"������Ϊ�գ�");
            return;
        }
        if(sceneMap.ContainsKey(state.ID))
        {
            Debug.LogError("����״̬" + state.ID + "�Ѵ��ڣ��޷��ظ���ӣ�");
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
    /// �Ƴ�״̬
    /// </summary>
    /// <param name="id"></param>
    public void RemoveState(string id)
    {
        if (id == null)
        {
            Debug.LogError("Ҫ�Ƴ���״̬ID����Ϊ�գ�");
            return;
        }
        if(!sceneMap.ContainsKey(id))
        {
            Debug.Log("Ҫ�Ƴ���״̬ID�����ڣ�");
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
    /// ת��״̬
    /// </summary>
    /// <param name="id"></param>
    public void TransitionState(string id)
    {
        if(id == null)
        {
            Debug.LogError("Ҫת����״̬"+id+"����Ϊ�գ�");
            return;
        }
        if(!sceneMap.ContainsKey(id))
        {
            Debug.LogError("Ҫת����״̬"+id+"�����ڣ�");
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
