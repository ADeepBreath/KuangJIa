using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Skill
{
    public List<SkillAnimationClip> animationClips = new List<SkillAnimationClip>();
    public List<SkillParticle> particles = new List<SkillParticle>();
    public List<SkillAudioClip> audioClips = new List<SkillAudioClip>();
    public List<SkillDamage> damages = new List<SkillDamage>();

    /// <summary>
    /// ��ռ���
    /// </summary>
    public void Clear()
    {
        if (animationClips.Count > 0)
        {
            animationClips.Clear();
        }
        if (particles.Count > 0)
        {
            particles.Clear();
        }
        if (audioClips.Count > 0)
        {
            audioClips.Clear();
        }
        if (damages.Count > 0)
        {
            damages.Clear();
        }
    }
}
/// <summary>
/// ���ܶ���
/// </summary>
public class SkillAnimationClip
{
    public AnimationClip clip;
    public float time;
}
/// <summary>
/// ������Ч
/// </summary>
public class SkillParticle
{
    public SkillType skillType;
    public GameObject particle;
    public float time;
    public float[] position = new float[3];
    public float[] angle = new float[3];
}
/// <summary>
/// ��������
/// </summary>
public class SkillAudioClip
{
    public AudioClip clip;
    public float time;
}
/// <summary>
/// �����˺�
/// </summary>
public class SkillDamage
{
    public int damage;
    public float time;
    public float distance;
    public float angle;
}


