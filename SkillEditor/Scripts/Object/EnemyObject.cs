using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : ObjectBase
{
    //���
    public PlaySkillComponent m_playSkillComponent;
    public List<string> skillNames = new List<string>();
    HpSliderComponent hpSliderComponent;

    public EnemyObject(ObjectInfoBase info) : base(info)
    {

    }

    public override void CreateObj()
    {
        base.CreateObj();

    }
    public override void OnCreate()
    {
        base.OnCreate();
        //��Ӽ������
        m_go.AddComponent<AudioSource>();
        m_go.AddComponent<PlaySkillComponent>().Init();
        m_playSkillComponent = m_go.GetComponent<PlaySkillComponent>();
        foreach (var item in m_playSkillComponent.skills.Keys)
        {
            skillNames.Add(item);
        }
        //���״̬��
        m_go.AddComponent<EnemyStateComponent>().enemy = this;
        //���Ѫ��
        m_go.AddComponent<HpSliderComponent>().Init(this);
        hpSliderComponent = m_go.GetComponent<HpSliderComponent>();
    }

    /// <summary>
    /// �ͷż���
    /// </summary>
    /// <param name="skillName"></param>
    public void PlaySkill(string skillName)
    {
        m_playSkillComponent.PlaySkill(skillName);
    }

    /// <summary>
    ///  �ı�Ѫ��
    /// </summary>
    /// <param name="hp"></param>
    public void ChangeHp(int hp)
    {
        (m_info as EnemyInfo).hp += hp;
        if ((m_info as EnemyInfo).hp < 0)
        {
            (m_info as EnemyInfo).hp = 0;
            DeleteObj();
        }
        hpSliderComponent.RefreshHpView();
    }
    public override void DeleteObj()
    {
        base.DeleteObj();
        m_animator.SetTrigger("Death");
        GameObject.Destroy(m_go, 1f);
    }
}
