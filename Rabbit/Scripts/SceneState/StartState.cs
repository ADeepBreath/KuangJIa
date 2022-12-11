using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ¿ªÊ¼×´Ì¬
/// </summary>
public class StartState : ISceneState
{
    public StartState(SceneStateCollecter sceneStateCollecter) : base(sceneStateCollecter)
    {
        this.ID = "StartScene";
    }

    public override void Reason()
    {
        m_sceneStateCollecter.TransitionState("GameScene");
    }

    public override void Update()
    {
        
    }
}
