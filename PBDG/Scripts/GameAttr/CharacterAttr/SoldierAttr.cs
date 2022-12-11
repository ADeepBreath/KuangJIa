using UnityEngine;
using System.Collections;

// Soldier數值
public class SoldierAttr : ICharacterAttr
{
	protected int 	m_SoldierLv; // Soldier等級
	protected int	m_AddMaxHP; // 因等级新增的HP

	public SoldierAttr()
	{}

	// 设定角色數值
	public void SetSoldierAttr(BaseAttr BaseAttr)
	{
		// 共用元件
		base.SetBaseAttr( BaseAttr );

		// 外部参数
		m_SoldierLv = 1;
		m_AddMaxHP = 0;
	}
	
	// 设定等级
	public void SetSoldierLv(int Lv)
	{
		m_SoldierLv = Lv;
	}

	// 取得等级
	public int GetSoldierLv()
	{
		return m_SoldierLv ;
	}

	// 最大HP
	public override int GetMaxHP()
	{
		return base.GetMaxHP() + m_AddMaxHP;
	}

	// 设定新增的最大生命力
	public void AddMaxHP(int AddMaxHP)
	{
		m_AddMaxHP = AddMaxHP;
	}



}
