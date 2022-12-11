using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();
    string activeName="StartPanel";

    // Start is called before the first frame update
    public void Start()
    {
        GameObject Canvas = GameObject.Find("Canvas");
        for (int i = 0; i < Canvas.transform.childCount; i++)
        {
            GameObject panel = Canvas.transform.GetChild(i).gameObject;
            panels.Add(panel.name, panel);
        }
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
    /// <summary>
    /// 激活面板
    /// </summary>
    public void ActivePanel(string panelName)
    {
        if(panels.ContainsKey(activeName))
        {
            panels[activeName].gameObject.SetActive(false);
        }
        panels[panelName].gameObject.SetActive(true);
        activeName = panelName;
    }
    /// <summary>
    /// 失活面板
    /// </summary>
    public void UnActivePanel(string panelName)
    {
        panels[panelName].gameObject.SetActive(false);
    }
}
