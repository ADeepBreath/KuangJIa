using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static bool isRun;//�ƶ�
    public static Vector2 pos;//ʵʱλ��
    Vector2 startpos;//��ʼλ��
    int len = 50;

    //�ƶ�
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
    //��ʼ����
    public void OnPointerDown(PointerEventData eventData)
    {
        startpos = eventData.position;
        isRun = true;
    }
    //���̧��
    public void OnPointerUp(PointerEventData eventData)
    {
        isRun = false;
        transform.localPosition = Vector3.zero;
    }
}