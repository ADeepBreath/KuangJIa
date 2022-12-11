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
        //设置相机看向玩家
        VirtualCameraManager.GetInstance().SelectVC("GameVC",m_go.transform);
    }

    public override void OnCreate()
    {
        base.OnCreate();
    }

    /// <summary>
    /// 摇杆移动
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
    ///  改变血量
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
    /// 改变蓝量
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
