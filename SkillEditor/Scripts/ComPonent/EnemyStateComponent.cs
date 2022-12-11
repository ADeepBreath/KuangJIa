using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// µÐÈË×´Ì¬»ú×é¼þ
/// </summary>
public class EnemyStateComponent : MonoBehaviour
{
    StateSystem stateSystem = new StateSystem();

    MyPlayer player;
    public EnemyObject enemy;

    float timer = 0,akTime=0;
    // Start is called before the first frame update
    void Start()
    {
        player = ObjectManager.GetInstance().myPlayerObj;

        stateSystem.states.Add(StateType.Idle, Idle);
        stateSystem.states.Add(StateType.Walk, Walk);
        stateSystem.states.Add(StateType.Follow, Follow);
        stateSystem.states.Add(StateType.Attack, Attack);
    }

    // Update is called once per frame
    void Update()
    {
        stateSystem.Update();

        if(enemy.m_go)
        {
            //Íæ¼ÒÔÚ¾¯½ä·¶Î§
            if (Vector3.Distance(player.m_go.transform.position,enemy.m_go.transform.position)<=(enemy.m_info as EnemyInfo).distance)
            {
                if(Vector3.Distance(player.m_go.transform.position, enemy.m_go.transform.position)>2f)
                {
                    stateSystem.type = StateType.Follow;
                }
                else
                {
                    stateSystem.type = StateType.Attack;
                }    
            }
            else
            {
                timer += Time.deltaTime;
                if (timer > 3f)
                {
                    timer -= 3f;
                    if (Random.Range(0, 3) == 0)
                    {
                        stateSystem.type = StateType.Idle;
                    }
                    else
                    {
                        stateSystem.type = StateType.Walk;
                    }
                }
            } 
        }
    }

    /// <summary>
    /// ´ý»ú
    /// </summary>
    void Idle()
    {
        enemy.m_animator.SetBool("Run", false);
        if (enemy.m_navMeshAgent.remainingDistance > 0)
        {
            enemy.m_navMeshAgent.SetDestination(transform.position);
        }
    }
    /// <summary>
    /// Ñ²Âß
    /// </summary>
    void Walk()
    {
        enemy.m_animator.SetBool("Run", true);
        if (enemy.m_navMeshAgent.remainingDistance <0.5f)
        {
            Vector3 scope = Random.insideUnitCircle * (enemy.m_info as EnemyInfo).distance;
            enemy.m_navMeshAgent.SetDestination(new Vector3(enemy.m_info.position[0]+scope.x, 0, enemy.m_info.position[2]+scope.y)); ;
        }
    }
    /// <summary>
    /// ¸úËæ
    /// </summary>
    void Follow()
    {
        enemy.m_animator.SetBool("Run", true);
        enemy.m_navMeshAgent.SetDestination(player.m_go.transform.position);
    }
    /// <summary>
    /// ¹¥»÷
    /// </summary>
    void Attack()
    {
        enemy.m_animator.SetBool("Run", false);
        enemy.m_go.transform.LookAt(player.m_go.transform.position);
        if (enemy.m_navMeshAgent.remainingDistance > 0)
        {
            enemy.m_navMeshAgent.SetDestination(transform.position);
        }

        akTime += Time.deltaTime;
        if (akTime >3f)
        {
            akTime -= 3f;
            enemy.PlaySkill(enemy.skillNames[Random.Range(0, enemy.skillNames.Count)]);
        }

    }
}
