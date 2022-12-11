using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillParticleComponent :SkillComponentBase
{
    public SkillType skillType;
    public GameObject particle;
    public float time;
    public float[] position;
    public float[] angle;

    public SkillParticleComponent(GameObject model, SkillType skillType, GameObject particle, float time, float[] position, float[] angle) : base(model)
    {
        base.model = model;
        this.skillType = skillType;
        this.particle = particle;
        this.time = time;
        this.position = position;
        this.angle = angle;
    }

    public override void Init()
    {
        base.Init();

    }
    public override void Play()
    {
        if(particle)
        {
            base.Play();
            TimeTool.GetInstance().AddTimeInvoke(startTime, time, PlayParticle);
        }

    }
    public override void Stop()
    {
        base.Stop();

    }
    /// <summary>
    /// 播放特效
    /// </summary>
    public void PlayParticle()
    {
        GameObject particle = GameObject.Instantiate(this.particle);
        particle.transform.SetParent(model.transform);
        particle.transform.localPosition = new Vector3(position[0], position[1], position[2]);
        particle.transform.localEulerAngles = new Vector3(angle[0], angle[1], angle[2]);
        if(skillType==SkillType.远程)
        {
            particle.transform.parent = null;
            GameObject.Destroy(particle, 3f);
            if(!particle.transform.GetComponent<Rigidbody>())
            {
                particle.transform.AddComponent<Rigidbody>();
                particle.transform.GetComponent<Rigidbody>().useGravity = false;
            }
            particle.transform.GetComponent<Rigidbody>().AddForce(particle.transform.forward * 1000);
        }
        else
        {
            GameObject.Destroy(particle, 0.8f);
        }
    }
}
