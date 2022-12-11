using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 技能
/// </summary>
public class Skill
{
    public List<SkillAnimationClip> animationClips = new List<SkillAnimationClip>();
    public List<SkillParticle> particles = new List<SkillParticle>();
    public List<SkillAudioClip> audioClips = new List<SkillAudioClip>();
    public List<SkillDamage> damages = new List<SkillDamage>();

    /// <summary>
    /// 清空技能
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
/// 技能动画
/// </summary>
public class SkillAnimationClip
{
    public AnimationClip clip;
    public float time;
}
/// <summary>
/// 技能特效
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
/// 技能声音
/// </summary>
public class SkillAudioClip
{
    public AudioClip clip;
    public float time;
}
/// <summary>
/// 技能伤害
/// </summary>
public class SkillDamage
{
    public int damage;
    public float time;
    public float distance;
    public float angle;
}


