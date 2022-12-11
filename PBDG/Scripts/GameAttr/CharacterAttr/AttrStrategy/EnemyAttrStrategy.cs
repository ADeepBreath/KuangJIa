using UnityEngine;
using System.Collections;

/// <summary>
/// 敌方单位数值的计算策略
/// </summary>
public class EnemyAttrStrategy : IAttrStrategy 
{
	// 初始的數值
	public override void InitAttr( ICharacterAttr CharacterAttr )
	{
		// 不用计算
	}
	
	//攻击加成
	public override int GetAtkPlusValue( ICharacterAttr CharacterAttr )
	{
		// 是否为地方数值
		EnemyAttr theEnemyAttr = CharacterAttr as EnemyAttr;
		if(theEnemyAttr==null)
			return 0;

		// 根据暴击率回传攻击加成值
		int RandValue =  UnityEngine.Random.Range(0,100);
		if( theEnemyAttr.GetCritRate()  >= RandValue )
		{
			theEnemyAttr.CutdownCritRate();		// 减少暴击率
			return theEnemyAttr.GetMaxHP()*5; 	// 血量的5倍值
		}
		return 0;
	}
	
	// 取得减伤害值
	public override int GetDmgDescValue( ICharacterAttr CharacterAttr )
	{
		// 没有减伤
		return 0;
	}

}
