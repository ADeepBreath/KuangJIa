using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamageComponent : SkillComponentBase
{
    public int damage;
    public float time;
    public float distance;
    public float angle;

    public SkillDamageComponent(GameObject model, int damage, float time, float distance, float angle) : base(model)
    {
        base.model = model; 
        this.damage = damage;
        this.time = time;
        this.distance = distance;
        this.angle = angle;
    }

    public override void Init()
    {
        base.Init();

    }
    public override void Play()
    {
        if(GameManager.GetInstance().pattern==2)
        {
            base.Play();
            TimeTool.GetInstance().AddTimeInvoke(startTime, time, PlayDamage);
        }   
    }
    public override void Stop()
    {
        base.Stop();

    }
    /// <summary>
    /// ¹¥»÷
    /// </summary>
    public void PlayDamage()
    {
        Vector3 tagetPos, modelPos;
        //Íæ¼Ò¹¥»÷
        if (model.name==ObjectManager.GetInstance().myPlayerObj.m_go.name)
        {
            List<EnemyObject> enemys = ObjectManager.GetInstance().enemys;
            for (int i = enemys.Count-1; i >=0; i--)
            {
                if (enemys[i].m_go)
                {
                    tagetPos = enemys[i].m_go.transform.position;
                    modelPos = model.transform.position;
                    if (Vector3.Distance(tagetPos, modelPos) <= distance)
                    {
                        float ang = Vector3.Angle(tagetPos - modelPos, model.transform.forward);
                        if (ang <= angle / 2)
                        {
                            enemys[i].ChangeHp(-damage);
                        }
                    }
                }
            }
        }
        //¹ÖÎï¹¥»÷
        else
        {
            MyPlayer myPlayer = ObjectManager.GetInstance().myPlayerObj;
            if(myPlayer.m_go)
            {
                tagetPos = myPlayer.m_go.transform.position;
                modelPos = model.transform.position;
                if (Vector3.Distance(tagetPos, modelPos) <= distance)
                {
                    float ang = Vector3.Angle(tagetPos - modelPos, model.transform.forward);
                    if (ang <= angle / 2)
                    {
                        myPlayer.ChangeHp(-damage);
                    }
                }
            }
        }
    }
}
