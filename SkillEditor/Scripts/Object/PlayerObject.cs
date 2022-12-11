using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerObject : ObjectBase
{
    //���
    public PlaySkillComponent m_playSkillComponent;
    public List<string> skillNames = new List<string>();

    public PlayerObject(ObjectInfoBase info) : base(info)
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
        m_playSkillComponent =m_go.GetComponent<PlaySkillComponent>();
        foreach (var item in m_playSkillComponent.skills.Keys)
        {
            skillNames.Add(item);
        }
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
    /// ˢ��Ѫ��������ʾ
    /// </summary>
    public virtual void RefreshHpAndMpView()
    {

    }
    public override void DeleteObj()
    {
        base.DeleteObj();
        m_animator.SetTrigger("Death");
        GameObject.Destroy(m_go,1f);
    }
}
