using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �����������
/// </summary>
public enum SkillComponentType
{
    Anim,
    Audio,
    Damage,
    Particle
}
/// <summary>
/// �ͷż������
/// </summary>
public class PlaySkillComponent : MonoBehaviour
{
    public Dictionary<string, Dictionary<SkillComponentType, List<SkillComponentBase>>> skillComponents = new Dictionary<string, Dictionary<SkillComponentType, List<SkillComponentBase>>>();
    public Dictionary<string, Skill> skills = new Dictionary<string, Skill>();

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        ReadJsonToSkill();
    }
    /// <summary>
    /// д�뼼��
    /// </summary>
    public void SetSkill(string SkillName,Skill skill)
    {
        if(!skills.ContainsKey(SkillName))
        {
            skills.Add(SkillName, new Skill());
        }
        skills[SkillName] = skill;
        skillComponents.Clear();
        SkillsToSkillComponents();
    }
    /// <summary>
    /// �ͷż���
    /// </summary>
    /// <param name="SkillName"></param>
    public void PlaySkill(string SkillName)
    {
        foreach (var skillComponents in skillComponents[SkillName].Values)
        {
            foreach (var skillComponent in skillComponents)
            {
                skillComponent.Play();
            }
        }
    }

    /// <summary>
    /// ��ȡ�������ñ�
    /// </summary>
    void ReadJsonToSkill()
    {
        if (File.Exists(gameObject.name + "Skills.json"))
        {
            string s = File.ReadAllText(gameObject.name + "Skills.json");
            Dictionary<string, SkillInfos> skillInfos = JsonConvert.DeserializeObject<Dictionary<string, SkillInfos>>(s);
            SkillInfosToSkills(skillInfos);
        }
    }
    /// <summary>
    /// ����ת�������
    /// </summary>
    public void SkillsToSkillComponents()
    {
        foreach (var skill in skills)
        {
            if (!skillComponents.ContainsKey(skill.Key))
            {
                skillComponents.Add(skill.Key, new Dictionary<SkillComponentType, List<SkillComponentBase>>());
            }

            //��Ӷ������
            if(skill.Value.animationClips.Count>0)
            {
                skillComponents[skill.Key].Add(SkillComponentType.Anim, new List<SkillComponentBase>());
                foreach (var animationClip in skill.Value.animationClips)
                {
                    skillComponents[skill.Key][SkillComponentType.Anim].Add(new SkillAnimationClipComponent(gameObject, animationClip.clip, animationClip.time));
                }
            }

            //�����Ч���
            if (skill.Value.particles.Count > 0)
            {
                skillComponents[skill.Key].Add(SkillComponentType.Particle, new List<SkillComponentBase>());
                foreach (var particle in skill.Value.particles)
                {
                    skillComponents[skill.Key][SkillComponentType.Particle].Add(new SkillParticleComponent(gameObject, particle.skillType,particle.particle,particle.time,particle.position,particle.angle));   
                }
            }

            //�����Ч���
            if (skill.Value.audioClips.Count > 0)
            {
                skillComponents[skill.Key].Add(SkillComponentType.Audio, new List<SkillComponentBase>());
                foreach (var audioClip in skill.Value.audioClips)
                {
                    skillComponents[skill.Key][SkillComponentType.Audio].Add(new SkillAudioClipComponent(gameObject,audioClip.clip,audioClip.time));
                }
            }

            //����˺����
            if (skill.Value.damages.Count > 0)
            {
                skillComponents[skill.Key].Add(SkillComponentType.Damage, new List<SkillComponentBase>());
                foreach (var damage in skill.Value.damages)
                {
                    skillComponents[skill.Key][SkillComponentType.Damage].Add(new SkillDamageComponent(gameObject,damage.damage,damage.time,damage.distance,damage.angle));
                }
            }
        }
    }
    /// <summary>
    /// ������Ϣת���ɼ���
    /// </summary>
    void SkillInfosToSkills(Dictionary<string, SkillInfos> skillInfos)
    {
        skills.Clear();
        foreach (var skillInfo in skillInfos)
        {
            //ת��������Ϣ
            skills.Add(skillInfo.Key, new Skill());
            foreach (var animationClipInfo in skillInfo.Value.animationClips)
            {
                SkillAnimationClip skillAnimationClip = new SkillAnimationClip();
                skillAnimationClip.clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(animationClipInfo.clip);
                skillAnimationClip.time = animationClipInfo.time;

                skills[skillInfo.Key].animationClips.Add(skillAnimationClip);
            }
            //ת����Ч��Ϣ
            foreach (var particleInfo in skillInfo.Value.particles)
            {
                SkillParticle skillParticle = new SkillParticle();
                skillParticle.skillType = particleInfo.skillType;
                skillParticle.particle = AssetDatabase.LoadAssetAtPath<GameObject>(particleInfo.particle);
                skillParticle.time = particleInfo.time;
                skillParticle.position = particleInfo.position;
                skillParticle.angle = particleInfo.angle;

                skills[skillInfo.Key].particles.Add(skillParticle);
            }
            //ת����Ч��Ϣ
            foreach (var audioClipInfo in skillInfo.Value.audioClips)
            {
                SkillAudioClip skillAudioClip = new SkillAudioClip();
                skillAudioClip.clip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioClipInfo.clip);
                skillAudioClip.time = audioClipInfo.time;

                skills[skillInfo.Key].audioClips.Add(skillAudioClip);
            }
            //ת���˺���Ϣ
            foreach (var damageInfo in skillInfo.Value.damages)
            {
                SkillDamage skillDamage = new SkillDamage();
                skillDamage.damage = damageInfo.damage;
                skillDamage.time = damageInfo.time;
                skillDamage.distance = damageInfo.distance;
                skillDamage.angle = damageInfo.angle;

                skills[skillInfo.Key].damages.Add(skillDamage);
            }
        }
        SkillsToSkillComponents();
    }
}
