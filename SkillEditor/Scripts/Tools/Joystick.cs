using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static bool isRun;//移动
    public static Vector2 pos;//实时位置
    Vector2 startpos;//起始位置
    int len = 50;

    //移动
    public void OnDrag(PointerEventData eventData)
    {
        pos = eventData.position - startpos;
        if (pos.magnitude >= len)
        {
            Vector2 v2 = Vector2.ClampMagnitude(pos, len);
            transform.localPosition = v2;
        }
        else
        {
            transform.localPosition = pos;
        }
    }
    //初始按下
    public void OnPointerDown(PointerEventData eventData)
    {
        startpos = eventData.position;
        isRun = true;
    }
    //最后抬起
    public void OnPointerUp(PointerEventData eventData)
    {
        isRun = false;
        transform.localPosition = Vector3.zero;
    }
}