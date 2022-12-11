using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAudioClipComponent : SkillComponentBase
{
    public AudioClip clip;
    public float time;

    AudioSource audioSource;

    public SkillAudioClipComponent(GameObject model, AudioClip clip, float time) : base(model)
    {
        base.model = model;
        this.clip = clip;
        this.time = time;
    }

    public override void Init()
    {
        base.Init();
        audioSource = model.GetComponent<AudioSource>();
    }
    public override void Play()
    {
        if (clip)
        {
            base.Play();

            TimeTool.GetInstance().AddTimeInvoke(startTime, time, PlayAudioClip);
        }

    }
    public override void Stop()
    {
        base.Stop();     
    }
    /// <summary>
    /// ≤•∑≈“Ù–ß
    /// </summary>
    public void PlayAudioClip()
    {
        audioSource.clip= clip;
        audioSource.Play();
    }
}
