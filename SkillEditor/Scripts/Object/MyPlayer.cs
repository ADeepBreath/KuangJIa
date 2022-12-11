using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : PlayerObject
{
    public MyPlayer(ObjectInfoBase info) : base(info)
    {
        
    }
    public override void CreateObj()
    {
        base.CreateObj();
        //��������������
        VirtualCameraManager.GetInstance().SelectVC("GameVC",m_go.transform);
    }

    public override void OnCreate()
    {
        base.OnCreate();
    }

    /// <summary>
    /// ҡ���ƶ�
    /// </summary>
    public void MoveByJoystick()
    {
        if(Joystick.isRun)
        {
            if(Joystick.pos.y > 0)
            {
                m_go.transform.Translate(Vector3.forward * Time.deltaTime * 5f);
            }
            else
            {
                m_go.transform.Translate(Vector3.forward * Time.deltaTime * -5f);
            }
            m_go.transform.Rotate(Vector3.up * Time.deltaTime * Joystick.pos.x*0.5f);

            m_animator.SetBool("Run", true);
        }
        else
        {
            m_animator.SetBool("Run", false);
        }
    }

    public override void RefreshHpAndMpView()
    {
        base.RefreshHpAndMpView();
        GamePanel.instance.RefreshHpAndMpView();
    }
    /// <summary>
    ///  �ı�Ѫ��
    /// </summary>
    /// <param name="hp"></param>
    public void ChangeHp(int hp)
    {
        (m_info as MyPlayerInfo).hp += hp;
        if((m_info as MyPlayerInfo).hp<0)
        {
            (m_info as MyPlayerInfo).hp = 0;
            DeleteObj();
        }     
        RefreshHpAndMpView();
    }
    /// <summary>
    /// �ı�����
    /// </summary>
    /// <param name="mp"></param>
    public void ChangeMp(int mp)
    {
        (m_info as MyPlayerInfo).mp += mp;
        if ((m_info as MyPlayerInfo).mp < 0)
        {
            (m_info as MyPlayerInfo).mp = 0;
        }
        RefreshHpAndMpView();
    }
}
