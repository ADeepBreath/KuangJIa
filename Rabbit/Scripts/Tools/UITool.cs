using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class UITool
{
    private static GameObject m_CanvasObj;

    /// <summary>
    /// 查找UI
    /// </summary>
    /// <param name="UIName"></param>
    /// <returns></returns>
    public static GameObject FindUIGameObject(string UIName)
    {
        if (m_CanvasObj == null)
            m_CanvasObj = UnityTool.FindGameObject("Canvas");
        if (m_CanvasObj == null)
            return null;
        return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
    }

    /// <summary>
    /// 获取UI组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Container"></param>
    /// <param name="UIName"></param>
    /// <returns></returns>
    public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
    {
        // 找出子物件 
        GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
        if (ChildGameObject == null)
            return null;

        T tempObj = ChildGameObject.GetComponent<T>();
        if (tempObj == null)
        {
            Debug.LogWarning("元件[" + UIName + "]不是[" + typeof(T) + "]");
            return null;
        }
        return tempObj;
    }
}
