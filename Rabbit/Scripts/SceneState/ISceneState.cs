using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISceneState
{
    private string m_id = "ISceneState";
    protected SceneStateCollecter m_sceneStateCollecter=null;

    public string ID { get => m_id; set => m_id = value; }

    public ISceneState(SceneStateCollecter sceneStateCollecter)
    {
        m_sceneStateCollecter=sceneStateCollecter;
    }

    public virtual void Before() { }
    public virtual void After() { }
    public abstract void Update();
    public abstract void Reason();
}
