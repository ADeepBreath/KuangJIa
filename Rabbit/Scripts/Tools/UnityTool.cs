using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class UnityTool
{
    /// <summary>
    /// 寻找对象
    /// </summary>
    /// <param name="GameObjectName"></param>
    /// <returns></returns>
    public static GameObject FindGameObject(string GameObjectName)
    {
        GameObject obj = GameObject.Find(GameObjectName);
        if (obj == null)
        {
            Debug.LogWarning("场景中找不到GameObject[" + GameObjectName + "]");
            return null;
        }
        return obj;
    }
    /// <summary>
    /// 获取子物体
    /// </summary>
    /// <param name="Container"></param>
    /// <param name="gameobjectName"></param>
    /// <returns></returns>
    public static GameObject FindChildGameObject(GameObject Container, string gameobjectName)
    {
        if (Container == null)
        {
            Debug.LogError("NGUICustomTools.GetChild : Container =null");
            return null;
        }

        Transform pGameObjectTF = null;											


        // 是不是Container本身
        if (Container.name == gameobjectName)
            pGameObjectTF = Container.transform;
        else
        {
            // 找出所有子元件						
            Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.name == gameobjectName)
                {
                    if (pGameObjectTF == null)
                        pGameObjectTF = child;
                    else
                        Debug.LogWarning("Container[" + Container.name + "]下找出重覆的元件名稱[" + gameobjectName + "]");
                }
            }
        }

        // 都没有找到
        if (pGameObjectTF == null)
        {
            Debug.LogError("元件[" + Container.name + "]找不到子元件[" + gameobjectName + "]");
            return null;
        }

        return pGameObjectTF.gameObject;
    }
}
