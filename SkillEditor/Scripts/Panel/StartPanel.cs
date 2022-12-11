using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 选择编辑器模式
    /// </summary>
    public void SelectEditorPattern()
    {
        GameManager.GetInstance().pattern = 1;
        VirtualCameraManager.GetInstance().SelectVC("SkillEditorVC");
        UIManager.GetInstance().UnActivePanel("StartPanel");
        cube.SetActive(true);
    }
    /// <summary>
    /// 选择游戏模式
    /// </summary>
    public void SelectGamePattern()
    {
        ObjectManager.GetInstance().InitObject();
        if (ObjectManager.GetInstance().myPlayerObj!=null)
        {
            GameManager.GetInstance().pattern = 2;
            UIManager.GetInstance().ActivePanel("GamePanel");
        }
        else
        {
            Debug.Log("未编辑玩家信息，无法开始游戏");
        }    
    }
}
