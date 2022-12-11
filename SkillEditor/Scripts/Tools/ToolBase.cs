using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBase:Singleton<ToolBase>
{

    // Start is called before the first frame update
    public void Start()
    {
        TimeTool.GetInstance().Start();
    }

    // Update is called once per frame
    public void Update()
    {
        TimeTool.GetInstance().Update();
    }
}
