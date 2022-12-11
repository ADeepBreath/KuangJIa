using UnityEngine;
using System.Collections;

/// <summary>
/// 特效删除
/// </summary>
public class EffectDelete : MonoBehaviour
{
    public float m_RemoveTimer = 1.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_RemoveTimer -= Time.deltaTime;
        if (m_RemoveTimer <= 0)
            GameObject.Destroy(gameObject);
    }
}
