using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    SceneStateController m_sceneStateController;

    private void Awake()
    {
        //切换场景不删除
        DontDestroyOnLoad(this);

        //随机种子
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_sceneStateController = new SceneStateController();

        StartState startState = new StartState(m_sceneStateController);
        LoadState loadState = new LoadState(m_sceneStateController);
        MainMenuState mainMenuState = new MainMenuState(m_sceneStateController);
        BattleState battleState = new BattleState(m_sceneStateController);

        m_sceneStateController.AddState(startState);
        m_sceneStateController.AddState(loadState);
        m_sceneStateController.AddState(mainMenuState);
        m_sceneStateController.AddState(battleState);
    }

    // Update is called once per frame
    void Update()
    {
        m_sceneStateController.Update();
    }
}
