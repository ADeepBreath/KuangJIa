using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : ISceneState,IObserver
{
    private bool isGame;

    public GameState(SceneStateCollecter sceneStateCollecter) : base(sceneStateCollecter)
    {
        this.ID = "GameScene";
    }

    public override void Before()
    {
        isGame = false;
        Game.Instance.Initinal();

        MessageCenter.Instance.RegisterObservers(MessageID.Afresh, this);
    }
    public void HandleNotification(MessageID id)
    {
        switch(id)
        {
            case MessageID.Afresh:
                {
                    isGame = true;
                }
                break;
        }
    }
    public override void Reason()
    {
        if(isGame)
        {
            m_sceneStateCollecter.TransitionState("StartScene");
        }
    }

    public override void Update()
    {
        Game.Instance.Update();
    }
    public override void After()
    {
        Game.Instance.Release();
    }


}
