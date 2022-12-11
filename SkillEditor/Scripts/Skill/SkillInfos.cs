using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能类型
/// </summary>
public enum SkillType
{
    近战, 远程
}
/// <summary>
/// 技能信息
/// </summary>
public class SkillInfos
{   
    public List<SkillAnimationClipInfo> animationClips = new List<SkillAnimationClipInfo>();
    public List<SkillParticleInfo> particles = new List<SkillParticleInfo>();
    public List<SkillAudioClipInfo> audioClips = new List<SkillAudioClipInfo>();
    public List<SkillDamageInfo> damages = new List<SkillDamageInfo>();

    /// <summary>
    /// 清空技能信息
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
/// 技能动画信息
/// </summary>
public class SkillAnimationClipInfo
{
    public string clip;
    public float time;
}
/// <summary>
/// 技能特效信息
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
/// 技能声音信息
/// </summary>
public class SkillAudioClipInfo
{
    public string clip;
    public float time;
}
/// <summary>
/// 技能伤害信息
/// </summary>
public class SkillDamageInfo
{
    public int damage;
    public float time;
    public float distance;
    public float angle;
}
