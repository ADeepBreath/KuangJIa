using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSliderComponent : MonoBehaviour
{
    EnemyObject enemy;
    Slider hpSlider;
    Text hpText;
    int initialHp;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //血条跟随
        if(hpSlider)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
            hpSlider.transform.position = pos;
            if (pos.z < 0)
            {
                hpSlider.gameObject.SetActive(false);
            }
            else
            {
                hpSlider.gameObject.SetActive(true);
            }
        } 
    }
    /// <summary>
    /// 刷新血量的显示
    /// </summary>
    public void RefreshHpView()
    {
        hpSlider.value = (float)(enemy.m_info as EnemyInfo).hp / initialHp;
        hpText.text = (enemy.m_info as EnemyInfo).hp + "/" + initialHp;
        if((enemy.m_info as EnemyInfo).hp<=0)
        {
            Destroy(hpSlider.gameObject);
        }
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init(EnemyObject enemy)
    {
        this.enemy = enemy;
        initialHp = (enemy.m_info as EnemyInfo).hp;
        hpSlider = Instantiate(Resources.Load<Slider>("HpSlider"), GameObject.Find("Canvas").transform);
        hpText = hpSlider.transform.Find("Text").GetComponent<Text>();
        RefreshHpView();
    }
}
