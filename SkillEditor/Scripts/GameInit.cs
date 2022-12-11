using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    public static GameInit instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().Start();
        UIManager.GetInstance().Start();
        VirtualCameraManager.GetInstance().Start();
        ToolBase.GetInstance().Start();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.GetInstance().Update();
        UIManager.GetInstance().Update();
        VirtualCameraManager.GetInstance().Update();
        ToolBase.GetInstance().Update();
    }
}
