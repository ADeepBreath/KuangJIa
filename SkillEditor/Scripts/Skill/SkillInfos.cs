using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public enum SkillType
{
    ��ս, Զ��
}
/// <summary>
/// ������Ϣ
/// </summary>
public class SkillInfos
{   
    public List<SkillAnimationClipInfo> animationClips = new List<SkillAnimationClipInfo>();
    public List<SkillParticleInfo> particles = new List<SkillParticleInfo>();
    public List<SkillAudioClipInfo> audioClips = new List<SkillAudioClipInfo>();
    public List<SkillDamageInfo> damages = new List<SkillDamageInfo>();

    /// <summary>
    /// ��ռ�����Ϣ
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
/// ���ܶ�����Ϣ
/// </summary>
public class SkillAnimationClipInfo
{
    public string clip;
    public float time;
}
/// <summary>
/// ������Ч��Ϣ
/// </summary>
public class SkillParticleInfo
{
    public SkillType skillType;
    public string particle;
    public float time;
    public float[] position = new float[3];
    public float[] angle = new float[3];
}
/// <summary>
/// ����������Ϣ
/// </summary>
public class SkillAudioClipInfo
{
    public string clip;
    public float time;
}
/// <summary>
/// �����˺���Ϣ
/// </summary>
public class SkillDamageInfo
{
    public int damage;
    public float time;
    public float distance;
    public float angle;
}
