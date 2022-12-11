using UnityEngine;
using System.Collections;

// Enemy數值
public class EnemyAttr : ICharacterAttr
{
	protected int m_CritRate = 0; //暴击

	public EnemyAttr()
	{}

    // 设定角色數值(包含外部参数)
    public void SetEnemyAttr(EnemyBaseAttr EnemyBaseAttr)
	{
		// 共用元件
		base.SetBaseAttr( EnemyBaseAttr );

		// 外部参数
		m_CritRate = EnemyBaseAttr.GetInitCritRate();
	}
	
	// 暴击率
	public int GetCritRate()
	{
		return m_CritRate;
	}

	// 减少暴击率
	public void CutdownCritRate()
	{
		m_CritRate -= m_CritRate/2;
	}

}
