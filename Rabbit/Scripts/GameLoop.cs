using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    SceneStateCollecter sceneStateCollecter;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        sceneStateCollecter = new SceneStateCollecter();

        StartState startState=new StartState(sceneStateCollecter);
        GameState gameState = new GameState(sceneStateCollecter);

        sceneStateCollecter.AddState(startState);
        sceneStateCollecter.AddState(gameState);
    }

    
    void Update()
    {
        sceneStateCollecter.Update();
    }
}
