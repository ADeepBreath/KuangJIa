using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SkillAnimationClipComponent : SkillComponentBase
{
    public AnimationClip clip;
    public float time;

    Animator animator;
    RuntimeAnimatorController myController;

    public SkillAnimationClipComponent(GameObject model, AnimationClip clip, float time) : base(model)
    {
        base.model = model;
        this.clip = clip;
        this.time = time;
    }

    public override void Init()
    {
        base.Init();

        animator=model.GetComponent<Animator>();
        myController = animator.runtimeAnimatorController;
    }
    public override void Play()
    {
        if(clip)
        {
            base.Play();

            TimeTool.GetInstance().AddTimeInvoke(startTime, time, PlayAnimationClip);
        }

    }
    public override void Stop()
    {
        base.Stop();
    }
    /// <summary>
    /// ²¥·Å¶¯»­
    /// </summary>
    public void PlayAnimationClip()
    {
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        myController= Resources.Load<RuntimeAnimatorController>("HeroAnimatorController/" + model.name);
        overrideController.runtimeAnimatorController = myController;
        overrideController["Skill"] = clip;
        animator.runtimeAnimatorController = overrideController;
        animator.Play("Skill", 0, 0);
    }
}
