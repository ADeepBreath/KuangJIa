using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SkillComponentEditor : EditorWindow
{
    static string Name;
    static Skill skill = new Skill();

    //编辑器变量
    List<string> componentNames = new List<string>(){ "动画", "特效", "音效", "攻击" };
    int componentNameIndex = 0;

    Vector2 scroll;

    public static void Init(string skillName,SkillInfos skillInfos)
    {
        SkillComponentEditor win = GetWindow<SkillComponentEditor>(skillName);
        win.minSize = new Vector2(410, 600);
        win.maxSize= new Vector2(410, 600);
        win.Show();

        Name = skillName;
        SkillInfosToSkills(skillInfos);
    }
    private void OnGUI()
    {
        //选择添加组件
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("选择组件：", GUILayout.Width(60));
            componentNameIndex = EditorGUILayout.Popup(componentNameIndex, componentNames.ToArray());
            if(GUILayout.Button("Add"))
            {
                switch(componentNameIndex)
                {
                    case 0: skill.animationClips.Add(new SkillAnimationClip());break;
                    case 1: skill.particles.Add(new SkillParticle()); break;
                    case 2: skill.audioClips.Add(new SkillAudioClip()); break;
                    case 3: skill.damages.Add(new SkillDamage()); break;
                }
            }
        }
        GUILayout.EndHorizontal();

        scroll = GUILayout.BeginScrollView(scroll, false,true);
        {
            if (skill.animationClips.Count > 0)
            {
                GUILayout.Space(10);
                GUILayout.Label("——————————————动画组件——————————————");
                foreach (var animationClip in skill.animationClips)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("人物动画：", GUILayout.Width(60));
                        animationClip.clip = (AnimationClip)EditorGUILayout.ObjectField(animationClip.clip, typeof(AnimationClip), false, GUILayout.Width(180));
                        GUILayout.Label("延迟时间：", GUILayout.Width(60));
                        animationClip.time = EditorGUILayout.FloatField(animationClip.time, GUILayout.Width(60));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(8);
                }
                if (GUILayout.Button("删除一段动画组件"))
                {
                    skill.animationClips.RemoveAt(skill.animationClips.Count - 1);
                }
            }

            if (skill.particles.Count > 0)
            {
                GUILayout.Space(10);
                GUILayout.Label("——————————————特效组件——————————————");
                foreach (var particle in skill.particles)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("特效类型：", GUILayout.Width(60));
                        particle.skillType = (SkillType)EditorGUILayout.EnumPopup(particle.skillType, GUILayout.Width(180));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("攻击特效：", GUILayout.Width(60));
                        particle.particle = (GameObject)EditorGUILayout.ObjectField(particle.particle, typeof(GameObject), false, GUILayout.Width(180));
                        GUILayout.Label("延迟时间：", GUILayout.Width(60));
                        particle.time = EditorGUILayout.FloatField(particle.time, GUILayout.Width(60));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("特效位置：", GUILayout.Width(60));
                        GUILayout.Label("X：", GUILayout.Width(30));
                        particle.position[0] = EditorGUILayout.FloatField(particle.position[0], GUILayout.Width(60));
                        GUILayout.Label("Y：", GUILayout.Width(30));
                        particle.position[1] = EditorGUILayout.FloatField(particle.position[1], GUILayout.Width(60));
                        GUILayout.Label("Z：", GUILayout.Width(30));
                        particle.position[2] = EditorGUILayout.FloatField(particle.position[2], GUILayout.Width(60));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("特效角度：", GUILayout.Width(60));
                        GUILayout.Label("X：", GUILayout.Width(30));
                        particle.angle[0] = EditorGUILayout.FloatField(particle.angle[0], GUILayout.Width(60));
                        GUILayout.Label("Y：", GUILayout.Width(30));
                        particle.angle[1] = EditorGUILayout.FloatField(particle.angle[1], GUILayout.Width(60));
                        GUILayout.Label("Z：", GUILayout.Width(30));
                        particle.angle[2] = EditorGUILayout.FloatField(particle.angle[2], GUILayout.Width(60));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(8);
                }
                if (GUILayout.Button("删除一段特效组件"))
                {
                    skill.particles.RemoveAt(skill.particles.Count - 1);
                }
            }

            if (skill.audioClips.Count > 0)
            {
                GUILayout.Space(10);
                GUILayout.Label("——————————————音效组件——————————————");
                foreach (var audioClip in skill.audioClips)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("打击音效：", GUILayout.Width(60));
                        audioClip.clip = (AudioClip)EditorGUILayout.ObjectField(audioClip.clip, typeof(AudioClip), false, GUILayout.Width(180));
                        GUILayout.Label("延迟时间：", GUILayout.Width(60));
                        audioClip.time = EditorGUILayout.FloatField(audioClip.time, GUILayout.Width(60));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(8);
                }
                if (GUILayout.Button("删除一段音效组件"))
                {
                    skill.audioClips.RemoveAt(skill.audioClips.Count - 1);
                }
            }

            if (skill.damages.Count > 0)
            {
                GUILayout.Space(10);
                GUILayout.Label("——————————————攻击组件——————————————");
                foreach (var damage in skill.damages)
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("伤害：", GUILayout.Width(60));
                        damage.damage = EditorGUILayout.IntSlider(damage.damage, 1, 10, GUILayout.Width(250));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("攻击距离：", GUILayout.Width(60));
                        damage.distance = EditorGUILayout.Slider(damage.distance, 3, 10, GUILayout.Width(250));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("范围角度：", GUILayout.Width(60));
                        damage.angle = EditorGUILayout.Slider(damage.angle, 30, 360, GUILayout.Width(250));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("延迟时间：", GUILayout.Width(60));
                        damage.time = EditorGUILayout.FloatField(damage.time, GUILayout.Width(60));
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(8);
                }
                if (GUILayout.Button("删除一段攻击组件"))
                {
                    skill.damages.RemoveAt(skill.damages.Count - 1);
                }
            }
        }
        GUILayout.EndScrollView();

        //预览和保存
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        {
            GUILayout.Space(70);
            if (GUILayout.Button("预览", GUILayout.Width(100), GUILayout.Height(30)))
            {
                SkillEditor.go.GetComponent<PlaySkillComponent>().SetSkill(Name, skill);
               SkillEditor.go.GetComponent<PlaySkillComponent>().PlaySkill(Name);
            }
            GUILayout.Space(70);
            if (GUILayout.Button("保存", GUILayout.Width(100), GUILayout.Height(30)))
            {
                SkillInfos skillInfos= SkillsToSkillInfos();
                SkillEditor.SaveSkillInfos(Name, skillInfos);
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(5);
    }
    /// <summary>
    /// 技能转换成技能信息
    /// </summary>
    SkillInfos SkillsToSkillInfos()
    {
        SkillInfos skillInfos = new SkillInfos();
        //转换动画
        foreach (var animationClip in skill.animationClips)
        {
            SkillAnimationClipInfo skillAnimationClipInfo = new SkillAnimationClipInfo();
            skillAnimationClipInfo.clip = AssetDatabase.GetAssetPath(animationClip.clip);
            skillAnimationClipInfo.time = animationClip.time;
            
            skillInfos.animationClips.Add(skillAnimationClipInfo);
        }
        //转换特效
        foreach (var particle in skill.particles)
        {
            SkillParticleInfo skillParticleInfo = new SkillParticleInfo();
            skillParticleInfo.skillType = particle.skillType;
            skillParticleInfo.particle = AssetDatabase.GetAssetPath(particle.particle);
            skillParticleInfo.time = particle.time;
            skillParticleInfo.position = particle.position;
            skillParticleInfo.angle = particle.angle;

            skillInfos.particles.Add(skillParticleInfo);
        }
        //转换音效
        foreach (var audioClip in skill.audioClips)
        {
            SkillAudioClipInfo skillAudioClipInfo = new SkillAudioClipInfo();
            skillAudioClipInfo.clip = AssetDatabase.GetAssetPath(audioClip.clip);
            skillAudioClipInfo.time = audioClip.time;

            skillInfos.audioClips.Add(skillAudioClipInfo);
        }
        //转换伤害
        foreach (var damage in skill.damages)
        {
            SkillDamageInfo skillDamageInfo = new SkillDamageInfo();
            skillDamageInfo.damage = damage.damage;
            skillDamageInfo.time = damage.time;
            skillDamageInfo.distance = damage.distance;
            skillDamageInfo.angle = damage.angle;

            skillInfos.damages.Add(skillDamageInfo);
        }

        return skillInfos;
    }
    /// <summary>
    /// 技能信息转换成技能
    /// </summary>
    static void SkillInfosToSkills(SkillInfos skillInfos)
    {
        skill.Clear();
        //转换动画信息
        foreach (var animationClipInfo in skillInfos.animationClips)
        {
            SkillAnimationClip skillAnimationClip = new SkillAnimationClip();
            skillAnimationClip.clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(animationClipInfo.clip);
            skillAnimationClip.time = animationClipInfo.time;

            skill.animationClips.Add(skillAnimationClip);
        }
        //转换特效信息
        foreach (var particleInfo in skillInfos.particles)
        {
            SkillParticle skillParticle = new SkillParticle();
            skillParticle.skillType = particleInfo.skillType;
            skillParticle.particle = AssetDatabase.LoadAssetAtPath<GameObject>(particleInfo.particle);
            skillParticle.time = particleInfo.time;
            skillParticle.position = particleInfo.position;
            skillParticle.angle = particleInfo.angle;

            skill.particles.Add(skillParticle);
        }
        //转换音效信息
        foreach (var audioClipInfo in skillInfos.audioClips)
        {
            SkillAudioClip skillAudioClip = new SkillAudioClip();
            skillAudioClip.clip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioClipInfo.clip);
            skillAudioClip.time = audioClipInfo.time;

            skill.audioClips.Add(skillAudioClip);
        }
        //转换伤害信息
        foreach (var damageInfo in skillInfos.damages)
        {
            SkillDamage skillDamage = new SkillDamage();
            skillDamage.damage = damageInfo.damage;
            skillDamage.time = damageInfo.time;
            skillDamage.distance = damageInfo.distance;
            skillDamage.angle = damageInfo.angle;

            skill.damages.Add(skillDamage);
        }
    }
}
